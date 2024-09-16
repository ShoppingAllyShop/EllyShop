import React from "react";
import Search from "./Search";
import UserInfo from "./UserInfo";

const Header = ({ user }) => {
  return (
    <div className="sticky w-full flex-grow items-center justify-between shadow-sm md:px-6 2xl:px-11 grid grid-cols-2 h-[84px] bg-white">
      <div className="left-content relative w-full px-4 py-4 ">
        <Search />
      </div>
      <div className="right-content h-full relative content-center">
        <div className="user-login float-right h-full">
          {user && <UserInfo user={user}/>}
        </div>
      </div>
      {/* <div className="flex items-center gap-3 2xsm:gap-7"></div> */}
    </div>
  );
};

export default Header;
