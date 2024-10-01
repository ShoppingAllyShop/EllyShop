// import { USER_LOCAL_STORAGE_KEY } from "../constants/common";
// import { checkIsAdminPage } from "../utils/httpUtil";

// const useAuthService = () => {
//   const isAdminPage = checkIsAdminPage();

//   const getRefreshToken = () => {
//     const user = isAdminPage
//       ? JSON.parse(sessionStorage.getItem(USER_LOCAL_STORAGE_KEY))
//       : JSON.parse(localStorage.getItem(USER_LOCAL_STORAGE_KEY));
//     return user?.refreshToken;
//   };

//   const getAccessToken = () => {
//     const user = isAdminPage
//       ? JSON.parse(sessionStorage.getItem(USER_LOCAL_STORAGE_KEY))
//       : JSON.parse(localStorage.getItem(USER_LOCAL_STORAGE_KEY));
//     return user?.accessToken;
//   };

//   const updateAccessToken = (token) => {
//     let user = isAdminPage
//       ? JSON.parse(sessionStorage.getItem(USER_LOCAL_STORAGE_KEY))
//       : JSON.parse(localStorage.getItem(USER_LOCAL_STORAGE_KEY));
//     user.accessToken = token;

//     if (isAdminPage) {
//       sessionStorage.setItem(USER_LOCAL_STORAGE_KEY, JSON.stringify(user));
//     } else {
//       localStorage.setItem(USER_LOCAL_STORAGE_KEY, JSON.stringify(user));
//     }
//   };

//   const getStorageUser = () => {
//     return isAdminPage
//       ? JSON.parse(sessionStorage.getItem(USER_LOCAL_STORAGE_KEY))
//       : JSON.parse(localStorage.getItem(USER_LOCAL_STORAGE_KEY));
//   };

//   const setStorageUser = (user) => {
//     isAdminPage
//       ? sessionStorage.setItem(USER_LOCAL_STORAGE_KEY, JSON.stringify(user))
//       : localStorage.setItem(USER_LOCAL_STORAGE_KEY, JSON.stringify(user));
//   };

//   const removeStorageUser = () => {
//     isAdminPage
//       ? sessionStorage.removeItem(USER_LOCAL_STORAGE_KEY)
//       : localStorage.removeItem(USER_LOCAL_STORAGE_KEY);
//   };

//   return {
//     getRefreshToken,
//     getAccessToken,
//     updateAccessToken,
//     getStorageUser,
//     setStorageUser,
//     removeStorageUser,
//   };
// };

// export default useAuthService;
