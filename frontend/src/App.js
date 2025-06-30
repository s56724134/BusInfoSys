import './App.css';
import { BrowserRouter, Routes, Route } from 'react-router-dom';
import { ThemeProvider, createTheme } from '@mui/material/styles';
import CssBaseline from '@mui/material/CssBaseline';
import BusRouteInput from './pages/BusRouteInput';
import BusStopOfRoute from './pages/BusStopOfRoute';
import BusRemind from './pages/BusRemind';

const theme = createTheme({
  palette: {
    primary: {
      main: '#1976d2',
    },
    secondary: {
      main: '#dc004e',
    },
  },
});

function App() {
  return (
    <ThemeProvider theme={theme}>
      <CssBaseline />
      <BrowserRouter>
        <Routes>
          <Route path="/" element={<BusRouteInput />} />
          <Route path="/bus/stops" element={<BusStopOfRoute />} />
          <Route path="/bus/remind" element={<BusRemind />} />
        </Routes>
      </BrowserRouter>
    </ThemeProvider>
  );
}

export default App;
