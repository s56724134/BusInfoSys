// LIFF Service - 封裝 LIFF SDK 相關功能
class LiffService {
  constructor() {
    this.liffId = '2007424668-B8rkZdQN';
    this.clientId = '2007424668';
    this.isInitialized = false;
  }

  async initialize() {
    try {
      if (!window.liff) {
        throw new Error('LIFF SDK not loaded');
      }
      
      await window.liff.init({ liffId: this.liffId });
      this.isInitialized = true;
      console.log('LIFF initialized successfully');
      return true;
    } catch (error) {
      console.error('LIFF initialization failed:', error);
      throw error;
    }
  }

  isLoggedIn() {
    if (!this.isInitialized || !window.liff) {
      return false;
    }
    return window.liff.isLoggedIn();
  }

  async login(redirectUri = window.location.href) {
    if (!this.isInitialized || !window.liff) {
      throw new Error('LIFF not initialized');
    }
    
    window.liff.login({ redirectUri });
  }

  logout() {
    if (!this.isInitialized || !window.liff) {
      return;
    }
    
    window.liff.logout();
  }

  getIdToken() {
    if (!this.isInitialized || !window.liff || !this.isLoggedIn()) {
      return null;
    }
    
    return window.liff.getIDToken();
  }

  async verifyToken(idToken = null) {
    const token = idToken || this.getIdToken();
    
    if (!token) {
      throw new Error('No ID token available');
    }

    try {
      const response = await fetch('/api/lineuserverify', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json'
        },
        body: JSON.stringify({
          idToken: token,
          clientId: this.clientId
        })
      });

      if (response.status === 401 || response.status === 400) {
        console.warn('Token 無效或過期，需要重新登入');
        this.logout();
        throw new Error('Token invalid or expired');
      }

      const data = await response.json();
      console.log('Token 驗證成功:', data);
      return data;
    } catch (error) {
      console.error('Token verification failed:', error);
      throw error;
    }
  }

  async getUserProfile() {
    if (!this.isInitialized || !window.liff || !this.isLoggedIn()) {
      throw new Error('Not logged in');
    }
    
    try {
      return await window.liff.getProfile();
    } catch (error) {
      console.error('Failed to get user profile:', error);
      throw error;
    }
  }
}

// 建立單例實例
const liffService = new LiffService();

export default liffService;