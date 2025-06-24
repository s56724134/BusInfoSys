using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using api.Models.LineModel;
using api.Interfaces;
using api.Data;

namespace api.Services
{
    public class ReminderBackgroundService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<ReminderBackgroundService> _logger;
        public ReminderBackgroundService(IServiceProvider serviceProvider,
            ILogger<ReminderBackgroundService> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                // Create a new async scope to ensure services like ApplicationDbContext are resolved
                // with the correct lifetime. This avoids sharing scoped services across different
                // background tasks, which could cause issues due to their scoped lifecycle.
                await using (var scope = _serviceProvider.CreateAsyncScope())
                {
                    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                    var lineService = scope.ServiceProvider.GetRequiredService<ILineMessageService>();

                    var now = DateTime.Now;
                    // For Test because this not effiencency
                    var reminds = db.Reminds
                        .AsEnumerable()
                        .Where(r => ShouldSendNow(r, now))
                        .ToList();

                    foreach (var remind in reminds)
                    {
                        // 發送 LINE 通知
                        await lineService.PushTextMessageAsync(remind.UserId, "您設定的提醒即將到站");
                        // 若是一次性提醒，就標記為已送出
                        // if (!remind.RepeatWeekTime.Any())
                        //     remind.IsSent = true;
                    }
                    await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
                }
            }
        }
        private bool ShouldSendNow(Remind r, DateTime now)
        {
            var weekday = now.DayOfWeek.ToString().Substring(0, 3);
            if (r.RemindTime.RepeatWeekTime.Any() && !r.RemindTime.RepeatWeekTime.Contains(weekday))
            {
                return false;
            }

            var busTime = DateTime.Today.Add(TimeSpan.Parse(r.RemindTime.BusShowUpTime.Start));
            var remindTime = busTime.AddMinutes(-int.Parse(r.RemindTime.SelectedRemindTime));

            return now >= remindTime && now < remindTime.AddMinutes(1);
        }

    }
}
