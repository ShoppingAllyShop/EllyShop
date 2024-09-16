import { Navigate, useLocation } from 'react-router-dom';
import { STORAGE_TYPE, USER_LOCAL_STORAGE_KEY } from '../constants/common';
import useAuthService from '../api/useAuthService';
import storageUtil from '../utils/storageUtil';

const PrivateRoute = ({ children }) => {
  const authServices = useAuthService();
  const location = useLocation();

  //set page info to use common on other component
  const isAdminPage = location.pathname.toLowerCase().includes("admin");

  //Authen and route
  //const accessToken = authServices.getAccessToken(USER_LOCAL_STORAGE_KEY); // Lấy JWT từ localStorage
  const accessToken =  storageUtil.getItemProp(USER_LOCAL_STORAGE_KEY, 'accessToken', STORAGE_TYPE.SESSION) // Lấy JWT từ localStorage
  const path = isAdminPage ? '/admin/login' : '/login'
  return accessToken != null ? children : <Navigate to={path} />;
};

export default PrivateRoute;