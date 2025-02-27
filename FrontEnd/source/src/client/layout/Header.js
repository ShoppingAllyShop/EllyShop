import React from "react";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faUser, faCartShopping } from "@fortawesome/free-solid-svg-icons"; // Import các icon bạn cần
import "./Mainlayout.js";
import Navigation from "./Navigation.js";
import { Link } from "react-router-dom";

const Header = ({data}) => {
  const renderTopHeader = () => {
    return (
      <div className="top-header bg-gray-800">
        <div className="grid grid-cols-2 mx-auto max-w-[1700px] container:none  ">
          <div className="left-content text-white py-5">
            <a href="tel:0966.353.000" className="">
              CSKH: 0966.353.000 - 0906.636.000
            </a>
          </div>

          <div className="right-content p-2 justify-self-end w-2/3">
            <form className="max-w-lg">
              <div className="flex">
                <label
                  htmlFor="search-dropdown"
                  className="mb-2 text-sm font-medium text-gray-900 sr-only dark:text-white"
                >
                  Your Email
                </label>
                <div className="relative w-full">
                  <input
                    type="search"
                    id="search-dropdown"
                    className="block p-2.5 w-full z-20 text-sm text-gray-900 bg-gray-50 rounded-e-lg border-s-gray-50 border-s-2 border "
                    placeholder="Tìm Kiếm..."
                    required=""
                  />
                  <button
                    type="submit"
                    className="absolute top-0 end-0 p-2.5 text-sm font-medium h-full text-white bg-blue-700 rounded-e-lg border "
                  >
                    <i className="fa-solid fa-magnifying-glass"></i>
                    <span className="sr-only ">Search</span>
                  </button>
                </div>
                <FontAwesomeIcon
                  icon={faUser}
                  size="2x"
                  color="white"
                  className="mx-2 p-1"
                />
                {/* Sử dụng icon */}
                <FontAwesomeIcon
                  icon={faCartShopping}
                  size="2x"
                  color="white"
                  className="p-1"
                />
              </div>
            </form>
          </div>
        </div>
      </div>
    );
  };

  const renderBottomHeader = () => {
    return (
      <div className="bottom-header grid grid-cols-12 container:none mx-auto max-w-[1700px] ">
        <div className="col-span-3">
          <Link to="/" reloadDocument>
            <img
              src="https://mediaelly.sgp1.digitaloceanspaces.com/uploads/2020/12/06111440/logo-black.svg"
              alt="Logo"
            />
          </Link>
        </div>
        <div className="col-span-9">
          <Navigation data={data} />
        </div>
      </div>
    );
  };

  return (
    <div id="header-layout" className="relative">
      {renderTopHeader()}
      {renderBottomHeader()}
    </div>
  );
};

export default Header;
