import axios from "axios";
import { STORAGE_TYPE, USER_LOCAL_STORAGE_KEY } from "../constants/common";
import axiosBase from "./axiosBase";
import { useNavigate } from "react-router-dom";
import useAuthService from "./useAuthService";
import { useSelector } from "react-redux";
import storageUtil from "../utils/storageUtil";

const useAxiosAuth = () => {  
  const BASE_URL = process.env.REACT_APP_BASE_API_URL;
  const navigation = useNavigate();
  const authServices = useAuthService();
  const userInfo = useSelector((state) => state?.adminLayout?.user);
  
  const axiosPrivate = axios.create({
    baseURL: BASE_URL,
    headers: { "Content-Type": "application/json" },
  });

  axiosPrivate.interceptors.request.use(
    (config) => {
      //const token = authServices.getAccessToken(USER_LOCAL_STORAGE_KEY);
      const token = storageUtil.getItemProp(USER_LOCAL_STORAGE_KEY, 'accessToken', STORAGE_TYPE.SESSION)
      if (token) {
        config.headers.Authorization = `Bearer ${token}`;
      }
      return config;
    },
    (error) => Promise.reject(error)
  );

  axiosPrivate.interceptors.response.use(
    (response) => response,
    async (error) => {
      if (error.response.status === 401) {
        try {
          // Logic refresh token hoặc điều hướng người dùng đến trang đăng nhập
          //const refreshToken = authServices.getRefreshToken(USER_LOCAL_STORAGE_KEY);
          const refreshToken = storageUtil.getItemProp(USER_LOCAL_STORAGE_KEY, 'refreshToken', STORAGE_TYPE.SESSION)
          const requestRefreshTokenData = {
            RefreshToken: refreshToken,
            Email: userInfo.email,
            Role: {
              RoleName: userInfo.RoleName
            }            
          }
          const response = await axiosBase.post("user/refresh-token", requestRefreshTokenData );
          let newAccessToken = response.data.result.accessToken;
          
          storageUtil.updateItemProp(USER_LOCAL_STORAGE_KEY, 'accessToken', STORAGE_TYPE.SESSION)

          error.config.headers.Authorization = `Bearer ${newAccessToken}`;
          return axiosBase(error.config);
        } catch (refreshError) {
          // Renew access token failed do invalid refresh token -> chuyển hướng đăng nhập lại
          navigation("/admin/login");
        }
      }

      if (error.response.status === 403) {
        navigation("/admin/error", {state:{errorHeader:"Bạn không có quyền truy cập chức năng này"}})
      }
      //return Promise.reject(error);
    }
  );
  return axiosPrivate;
};

export default useAxiosAuth;
