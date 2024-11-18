import React from "react";
import Search from "./Search";
import UserInfo from "./UserInfo";
import HamburgerButton from "../../../components/HamburgerButton";

const Header = ({ user }) => {
  const handleClickBurgerButton = () => {
  };
  return (
    <div className="sticky w-full flex-grow items-center justify-between shadow-sm md:px-6 2xl:px-11 grid grid-cols-2 h-[84px] bg-white z-[999]">
      <div className="left-content flex items-center relative w-full h-full pl-4">
        <div className="mr-5 block lg:hidden">
          <HamburgerButton
            size={7}
            handleClick={() => handleClickBurgerButton()} 
          />
        </div>
        <Search />
      </div>
      <div className="right-content h-full relative content-center">
        <div className="user-login float-right h-full mr-2">
          {user && <UserInfo user={user}/>}
        </div>
      </div>
    </div>
  );
};

export default Header;
