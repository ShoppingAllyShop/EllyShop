import React from "react";
import { menuSidebarList } from "../../common/constants/dataUI";
import { Link } from "react-router-dom";
import MenuSidebar from "./MenuSidebar";

const Sidebar = () => { 

  const renderMainMenu = (item) => {
    return (
      <div className="menu-item" key={item.id}>
        <h2>
          <Link
            to={item.path}
            className="flex items-center justify-between w-full p-3 font-medium rtl:text-right text-gray-100 dark:border-gray-700 dark:text-gray-400
                    hover:bg-gray-700 dark:hover:bg-gray-800 gap-3 bg-black"
          >
            <span className="flex items-center">
              <i className={`fa fa-${item.icon} mr-2`} />
              {item.name}
            </span>
          </Link>
        </h2>
      </div>
    );
  };

  const renderMenuSidebarList = () => {
    return (
      <div
        id="menu-accordion"
        className="mb-6 flex flex-col"
        data-accordion="open"
        data-active-classes="text-gray-100"
        data-inactive-classes="text-white"
      >
        {menuSidebarList.map((item) => {
          if (item.name === "Trang chủ") {
            return renderMainMenu(item);
          }

          return (
            <MenuSidebar data={item}/>            
          );
        })}
      </div>
    );
  };
  // transition duration-600 -translate-x-96 sm:translate-x-0
  return (
    <div
      id="side-bar"
      className="no-scrollbar h-screen overflow-y-hidden bg-black ease-linear dark:bg-boxdark
      transition duration-600 -translate-x-96 sm:translate-x-0"
    >
      <div className="logo flex items-center justify-between gap-2 px-6 py-5.5 lg:py-6.5 h-[84px] bg-white">
        <img
          src="https://mediaelly.sgp1.digitaloceanspaces.com/uploads/2020/12/06111440/logo-black.svg"
          alt="ELLY – TOP 10 Thương Hiệu Nổi Tiếng Việt Nam"
        />
      </div>
      <div className="menu-bar flex flex-col overflow-y-auto duration-300 ease-linear no-scrollbar">
        <div className="funciton-box">{renderMenuSidebarList()}</div>
      </div>
    </div>
  );
};

export default Sidebar;
