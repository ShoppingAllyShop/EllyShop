import { USER_LOCAL_STORAGE_KEY } from "../constants/common";

export const getRefreshToken = () => {
  const user = JSON.parse(localStorage.getItem(USER_LOCAL_STORAGE_KEY));
  return user?.tokens?.refreshToken;
};

export const getAccessToken = () => {
  const user = JSON.parse(localStorage.getItem(USER_LOCAL_STORAGE_KEY));
  return user?.tokens?.accessToken;
};

export const updateAccessToken = (token) => {
  let user = JSON.parse(localStorage.getItem(USER_LOCAL_STORAGE_KEY));
  user.accessToken = token;
  localStorage.setItem(USER_LOCAL_STORAGE_KEY, JSON.stringify(user));
};

export const getLocalUser = () => {
  return JSON.parse(localStorage.getItem(USER_LOCAL_STORAGE_KEY));
};

export const setLocalUser = (user) => {
  localStorage.setItem(USER_LOCAL_STORAGE_KEY, JSON.stringify(user));
};

export const removeLocalUser = () => {
  localStorage.removeItem(USER_LOCAL_STORAGE_KEY);
};