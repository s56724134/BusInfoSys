import { useState, useEffect, useCallback } from 'react';
import liffService from '../services/liffService';

const useLiff = () => {
  const [isInitialized, setIsInitialized] = useState(false);
  const [isLoggedIn, setIsLoggedIn] = useState(false);
  const [userProfile, setUserProfile] = useState(null);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  const checkLoginStatus = useCallback(async () => {
    try {
      if (!isInitialized) return;
      
      const loggedIn = liffService.isLoggedIn();
      setIsLoggedIn(loggedIn);
      
      if (loggedIn) {
        // 驗證 token
        await liffService.verifyToken();
        
        // 取得使用者資料
        const profile = await liffService.getUserProfile();
        setUserProfile(profile);
      }
    } catch (err) {
      console.error('Login status check failed:', err);
      setError(err.message);
      setIsLoggedIn(false);
      setUserProfile(null);
    }
  }, [isInitialized]);

  const initialize = useCallback(async () => {
    try {
      setLoading(true);
      setError(null);
      
      await liffService.initialize();
      setIsInitialized(true);
      
    } catch (err) {
      console.error('LIFF initialization failed:', err);
      setError(err.message);
    } finally {
      setLoading(false);
    }
  }, []);

  const login = useCallback(async () => {
    try {
      setError(null);
      await liffService.login();
    } catch (err) {
      console.error('Login failed:', err);
      setError(err.message);
    }
  }, []);

  const logout = useCallback(() => {
    try {
      liffService.logout();
      setIsLoggedIn(false);
      setUserProfile(null);
    } catch (err) {
      console.error('Logout failed:', err);
      setError(err.message);
    }
  }, []);

  // 初始化 LIFF
  useEffect(() => {
    initialize();
  }, [initialize]);

  // 檢查登入狀態
  useEffect(() => {
    if (isInitialized) {
      checkLoginStatus();
    }
  }, [isInitialized, checkLoginStatus]);

  return {
    isInitialized,
    isLoggedIn,
    userProfile,
    loading,
    error,
    login,
    logout,
    checkLoginStatus
  };
};

export default useLiff;