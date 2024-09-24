import { Navigate, useLocation } from 'react-router-dom';
import { STORAGE_TYPE, USER_LOCAL_STORAGE_KEY } from '../constants/common';
import storageUtil from '../utils/storageUtil';
import { ADMIN_ENDPOINT } from '../constants/endpoint';
import { ROLE_ENUM } from '../constants/enum';

const PrivateRoute = ({ children }) => {
  const location = useLocation(); 
  const isAdminPage = location.pathname.toLowerCase().includes("admin");
  const path = isAdminPage ? '/admin/login' : '/login'

  //Authen and route
  //Admin page
  if (isAdminPage) {
    const user = storageUtil.getItemProp(USER_LOCAL_STORAGE_KEY, 'roleName', STORAGE_TYPE.SESSION)
    const accessToken = storageUtil.getItemProp(USER_LOCAL_STORAGE_KEY, 'accessToken', STORAGE_TYPE.SESSION) // Lấy JWT từ sessionStorage
    if (accessToken == null || user == null || JSON.stringify(user) === '{}') return <Navigate to={path}/>

    const isHasCustomerRole = user?.role?.roleName === ROLE_ENUM.CUSTOMER
    if (isHasCustomerRole) return <Navigate to={ADMIN_ENDPOINT.ERROR} state={{errorHeader:"Bạn không có quyền truy cập chức năng này"}}/>

    return children 
  }

  //Client page
  const accessToken =  storageUtil.getItemProp(USER_LOCAL_STORAGE_KEY, 'accessToken', STORAGE_TYPE.LOCAL) // Lấy JWT từ localStorage
  return accessToken != null ? children : <Navigate to={path} />
};

export default PrivateRoute;