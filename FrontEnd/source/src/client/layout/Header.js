import React from "react";
// src/index.js hoặc src/App.js
import "../../fontawsome"; // Import cấu hình Font Awesome
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
                <button
                  id="dropdown-button"
                  data-dropdown-toggle="dropdown"
                  className="flex-shrink-0 z-10 inline-flex items-center py-2.5 px-4 text-sm font-medium text-center text-gray-900 bg-gray-100 border border-gray-300 rounded-s-lg hover:bg-gray-200 
                   dark:bg-gray-700 dark:hover:bg-gray-600 dark:text-white dark:border-gray-600"
                  type="button"
                >
                  All
                  <svg
                    className="w-2.5 h-2.5 ms-2.5"
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
                      d="m1 1 4 4 4-4"
                    />
                  </svg>
                </button>
                <div
                  id="dropdown"
                  className="z-10 hidden bg-white divide-y divide-gray-100 rounded-lg shadow w-44 dark:bg-gray-700"
                >
                  <ul
                    className="py-2 text-sm text-gray-700 dark:text-gray-200"
                    aria-labelledby="dropdown-button"
                  >
                    <li>
                      <button
                        type="button"
                        className="inline-flex w-full px-4 py-2 hover:bg-gray-100 dark:hover:bg-gray-600 dark:hover:text-white"
                      >
                        Mockups
                      </button>
                    </li>
                    <li>
                      <button
                        type="button"
                        className="inline-flex w-full px-4 py-2 hover:bg-gray-100 dark:hover:bg-gray-600 dark:hover:text-white"
                      >
                        Templates
                      </button>
                    </li>
                    <li>
                      <button
                        type="button"
                        className="inline-flex w-full px-4 py-2 hover:bg-gray-100 dark:hover:bg-gray-600 dark:hover:text-white"
                      >
                        Design
                      </button>
                    </li>
                    <li>
                      <button
                        type="button"
                        className="inline-flex w-full px-4 py-2 hover:bg-gray-100 dark:hover:bg-gray-600 dark:hover:text-white"
                      >
                        Logos
                      </button>
                    </li>
                  </ul>
                </div>
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
                    <svg
                      className="w-4 h-4"
                      aria-hidden="true"
                      xmlns="http://www.w3.org/2000/svg"
                      fill="none"
                      viewBox="0 0 20 20"
                    >
                      <path
                        stroke="currentColor"
                        strokeLinecap="round"
                        strokeLinejoin="round"
                        strokeWidth={2}
                        d="m19 19-4-4m0-7A7 7 0 1 1 1 8a7 7 0 0 1 14 0Z"
                      />
                    </svg>
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
