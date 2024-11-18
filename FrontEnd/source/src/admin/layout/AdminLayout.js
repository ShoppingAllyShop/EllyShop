import React, { useEffect } from "react";
import Header from "./components/Header";
import { Outlet } from "react-router-dom";
import './AdminLayout.scss'
import Sidebar from "./components/Sidebar";
import { useSelector } from "react-redux";

const AdminLayout = () => {
  const user = useSelector((state) => state?.adminLayout?.user);

  useEffect(() => {
    const script = document.createElement('script');  
    script.src = 'https://cdn.jsdelivr.net/npm/flowbite@2.4.1/dist/flowbite.min.js';
    script.async = true;
    document.body.appendChild(script);
  
    return () => {
      document.body.removeChild(script);
    };
  }, []);
  
  return (
    <div id="admin-page" className="lg:grid lg:grid-cols-12 h-screen overflow-hidden">
      <div className="left-block relative hidden lg:col-span-2 lg:block">
        <Sidebar />
      </div>
      <div className="right-block relative w-full lg:col-span-10">
        <Header user={user}/>
        <main>
          <div className="overflow-y-auto overflow-x-hidden h-[calc(100vh-80px)]">
            <Outlet />
          </div>        
        </main>
      </div>
    </div>
  );
};

export default AdminLayout;
