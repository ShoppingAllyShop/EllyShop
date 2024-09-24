import React, { useEffect } from "react";
import Header from "./components/Header";
import { Outlet } from "react-router-dom";
import './AdminLayout.scss'
import Sidebar from "./components/Sidebar";
import { USER_LOCAL_STORAGE_KEY } from "../../constants/common";
import { useDispatch, useSelector } from "react-redux";
import { setUser } from "../../redux/slices/admin/adminLayoutSlice";
import useAuthService from "../../api/useAuthService";
import storageUtil from "../../utils/storageUtil";

const AdminLayout = () => {
  const authServices = useAuthService()
  const user = useSelector((state) => state?.adminLayout?.user);
  const dispatch = useDispatch();

  useEffect(() => {
    const script = document.createElement('script');
    script.src = 'https://cdn.jsdelivr.net/npm/flowbite@2.4.1/dist/flowbite.min.js';
    script.async = true;
    document.body.appendChild(script);

    return () => {
      document.body.removeChild(script);
    };
  }, []);


  useEffect(() => {
    //const userInfo = authServices.getStorageUser(USER_LOCAL_STORAGE_KEY);
    // const userInfo = storageUtil.getItem(USER_LOCAL_STORAGE_KEY);
    // console.log('cmm')
    // if (userInfo !== null) dispatch(setUser(userInfo));    
  }, []); 
   //transition duration-600 -translate-x-96 sm:translate-x-0
  return (
    <div id="admin-page" className="block lg:grid lg:grid-cols-12 h-screen overflow-hidden">
      <div className="relative hidden lg:col-span-2 lg:block ">
        <Sidebar />
      </div>
      <div className="relative w-full lg:col-span-10">
        <Header user={user}/>
        <main>
          <Outlet />
        </main>
      </div>
    </div>
  );
};

export default AdminLayout;
