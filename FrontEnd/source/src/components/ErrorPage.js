import React from "react";
import { ReactComponent as Illustration } from "../images/others/illustration-01.svg";
import { Link, useLocation } from "react-router-dom";

const ErrorPage = () => {
  const location = useLocation();
  const { errorHeader, errorMessage = null } = location.state || {};  // Lấy message từ state
  console.log('location.state',location.state)
  return (
    <div className="rounded-sm border border-stroke bg-white px-5 py-10 shadow-default dark:border-strokedark dark:bg-boxdark sm:py-20">
      <div className="mx-auto max-w-[410px]">
        <Illustration />
        <div className="mt-7.5 text-center">
          <h2 className="my-3 text-2xl font-bold text-black dark:text-white">
            Xin lỗi! {errorHeader}
          </h2>
          <p className="font-medium">
            {errorMessage !== null ? errorMessage : 'Vui lòng liên hệ quản trị viên hoặc gọi hotline 1900.9999 để được hỗ trợ. Xin cảm ơn !'}
          </p>
          <Link
            to={"/admin/dashboard"}
            type="button"
            className="text-white mt-5 bg-[#2557D6] hover:bg-[#2557D6]/90 focus:ring-4 focus:ring-[#2557D6]/50 focus:outline-none font-medium rounded-lg text-sm px-5 py-2.5 text-center inline-flex items-center dark:focus:ring-[#2557D6]/50 me-2 mb-2"
          >
            Quay về trang chủ
          </Link>          
        </div>
      </div>
    </div>
  );
};

export default ErrorPage;
