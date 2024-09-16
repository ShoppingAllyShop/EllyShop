import React, { useEffect, useRef, useState } from "react";
import { Link, useNavigate } from "react-router-dom";
import {
  BUTTON_NAME,
  STORAGE_TYPE,
  USER_LOCAL_STORAGE_KEY,
} from "../../../constants/common";
import { useDispatch } from "react-redux";
import { setUser } from "../../../redux/slices/admin/adminLayoutSlice";
import storageUtil from "../../../utils/storageUtil";

const UserInfo = ({ user }) => {
  const [isDisplayDropdown, setIsDisplayDropdown] = useState(false);
  const dropdownRef = useRef(null);
  const dispatch = useDispatch();
  const navigate = useNavigate();

  //xử lý sự kiện click chuột ra ngoài dropdown
  useEffect(() => {
    const handleClickOutside = (event) => {
      if (dropdownRef.current && !dropdownRef.current.contains(event.target)) {
        setIsDisplayDropdown(false);
      }
    };

    document.addEventListener("mousedown", handleClickOutside);
    return () => {
      document.removeEventListener("mousedown", handleClickOutside);
    };
  }, []);

  const renderDropdownIcon = () => {
    return (
      <>
        <svg
          data-accordion-icon=""
          className={`w-3 h-3 text-black rotate-180 shrink-0 transition-transform duration-200 transform ${
            !isDisplayDropdown ? "rotate-180" : "rotate-0"
          }`}
          aria-hidden="true"
          xmlns="http://www.w3.org/2000/svg"
          fill="none"
          viewBox="0 0 10 6"
        >
          <path
            stroke="currentColor"
            strokeLinecap="round"
            strokeLinejoin="round"
            strokeWidth={2}
            d="M9 5 5 1 1 5"
          />
        </svg>
      </>
    );
  };

  const handleClickUserInfo = () => {
    console.log("isDisplayDropdown", isDisplayDropdown);
    setIsDisplayDropdown(!isDisplayDropdown);
  };

  const handleLogout = () => {
    storageUtil.removeItem(USER_LOCAL_STORAGE_KEY, STORAGE_TYPE.SESSION);
    dispatch(setUser({}));
    navigate("login");
  };

  const renderDropdown = () => {
    return (
      <div
        id="user-info-droppdown"
        className="absolute right-0 top-full border w-56 z-50 bg-white"
      >
        <ul className="border-b py-4 px-6 space-y-4">
          <li className="hover:text-blue-500 hover:cursor-pointer">
            <Link>
              <i className={`fa fa-user mr-2`} /> {BUTTON_NAME.INFO}
            </Link>
          </li>
          <li className="hover:text-blue-500 hover:cursor-pointer">
            <Link>
              <i className={`fa fa-gear mr-2`} /> {BUTTON_NAME.SETTING}
            </Link>
          </li>
        </ul>
        <button
          className="py-4 px-6 hover:text-blue-500 w-full text-left"
          onClick={handleLogout}
        >
          <i className={`fa fa-right-from-bracket mr-2`} /> {BUTTON_NAME.LOGOUT}
        </button>
      </div>
    );
  };

  return (
    <div
      id="user-info"
      className="flex items-center gap-4 relative h-full"
      ref={dropdownRef}
    >
      <button
        type="button"
        className="flex items-center justify-between w-full font-medium rtl:text-right text-gray-100 dark:border-gray-700 dark:text-gray-400
           dark:hover:bg-gray-800 gap-3"
        onClick={handleClickUserInfo}
      >
        <span className="hidden text-right lg:block">
          <span className="block text-sm font-medium text-black dark:text-white">
            {user.userName}
          </span>
          <span className="block text-xs font-medium text-gray-500">
            {user.roleName}
          </span>
        </span>
        <span className="h-12 w-12 rounded-full">
          <img
            src="https://demo.tailadmin.com/src/images/user/user-01.png"
            alt="User"
          />
        </span>
        {renderDropdownIcon()}
      </button>
      {isDisplayDropdown && renderDropdown()}
    </div>
  );
};

export default UserInfo;
