import React, { useState, useRef, useEffect } from "react";
import { Link } from "react-router-dom";
import { getFirstCharsByLengthNumber } from "../../utils/stringUtil.js";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faChevronDown } from "@fortawesome/free-solid-svg-icons"; // Import các icon bạn cần
import { GUID_ID } from "../../constants/common.js";
const Navigation = (props) => {
  const { navigation, category, collection, production, discound } = props.data;
  const [isDisplayContentProductNav, setIsDisplayContentProductNav] =
    useState(false);

  //Hover navigation to show dropdown item
  const renderDropDown = (navigationItem, categoryList) => {
    const createId = getFirstCharsByLengthNumber(navigationItem.id, 8);
    const dropdownDelayButtonId = "dropdownDelayButton" + "-" + createId;
    const dropdownDelayId = "dropdownDelay-" + createId;
    const isProductNavigation = navigationItem.name === "Sản Phẩm";
    return (
      <li key={navigationItem.id} className="p-3 flex-none z-40 transition duration-400">
        <button
          id={dropdownDelayButtonId}
          data-dropdown-toggle={dropdownDelayId}
          data-dropdown-delay={500}
          data-dropdown-trigger="hover"
          className={`text-black rounded-lg text-sm text-center inline-flex items-center h-full
          ${
            navigationItem.id === "9beddd21-4193-463c-acc8-66dc3c76e568"
              ? "text-red-600"
              : ""
          }`}
          type="button"
        >
          {navigationItem.name}
          <svg
            className="w-2.5 h-2.5 ms-3 text-black"
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
        {/* Dropdown menu */}
        <div
          id={dropdownDelayId}
          className={`z-10 hidden bg-white divide-y divide-gray-100 rounded-lg shadow dark:bg-gray-700 ${
            isProductNavigation ? "w-full" : "w-44"
          } ${isProductNavigation ? "absolute left-0 top-full" : ""}`}
        >
          <ul
            className="sub-drop-down py-2 text-sm text-gray-700 dark:text-gray-200 "
            aria-labelledby={dropdownDelayButtonId}
          >
            {renderDropDownItem(categoryList)}
          </ul>
        </div>
      </li>
    );
  };

  const renderDropDownItem = (data) => {
    return data.map((item) => {
      return (
        <li key={item.id}>
          <Link href="#" className="block px-4 py-2 dark:hover:text-white ">
            {item.name}
          </Link>
        </li>
      );
    });
  };

  //render content of "Sản Phẩm" button
  const renderDropDownProductItem = (category) => {
    const levelOneCategory = category.filter(
      (category) => category.categoryLevel === 1
    );
    const levelTwoCategory = category.filter(
      (category) => category.categoryLevel === 2
    );
    return (
      <div
        id="product-category"
        className={`text-left border-2 shadow-lg absolute w-full left-0 top-full grid grid-cols-10 list-none bg-white z-40`}
      >
        {levelOneCategory.map((item) => {
          let levelTwoData = levelTwoCategory.filter(
            (category) => category.parentId === item.id
          );
          return (
            <li
              key={item.id}
              className="p-5 first-line:font-bold first-line:uppercase"
            >
              <Link href="#" className="dark:hover:text-white ">
                {item.name} 
              </Link>
              {renderLevelTwoData(levelTwoData)}
            </li>
          );
        })}
        <li className="discount-image">
          <Link href="" className="dropdown-image-column">
            <img
              className="w-180 h-480"
              src="https://elly.vn/wp-content/themes/ml/img/anh-megamenu.jpg"
              title="Image column"
              alt="Image column"
            />
          </Link>
        </li>
      </div>
    );
  };
  const renderLevelTwoData = (levelTwoData) => {
    return (
      <ul>
        {levelTwoData.map((item) => {
          return (
            <li key={item.id} className="border-t-2 py-2">
              <Link href="#" className="dark:hover:text-white ">
                {item.name}
              </Link>
            </li>
          );
        })}
      </ul>
    );
  };

  const renderProductNavigationItem = (item) => {
    return (
      <li key={item.id} className="flex h-full">
        <button
          onMouseEnter={() => setIsDisplayContentProductNav(true)}
          onMouseLeave={() => setIsDisplayContentProductNav(false)}
          className="h-full p-3 "
        >
          {item.name} <FontAwesomeIcon className="text-xs" icon={faChevronDown} />
          {isDisplayContentProductNav && renderDropDownProductItem(category)}
        </button>
      </li>
    );
  };

  return (
    <>
      <ul className="flex items-center h-full">
        {navigation.map((item) => {
          //render "Woman" navigation
          if (item.id === GUID_ID.WOMAN_NAV) {
            const womanCategory = category.filter(
              (category) =>
                category.gender === false && category.categoryLevel === 2
            );
            return renderDropDown(item, womanCategory);
          } else if (item.id === GUID_ID.MAN_NAV) {
            //render "Man" navigation
            const manCategory = category.filter(
              (category) =>
                category.gender === true && category.categoryLevel === 2
            );
            return renderDropDown(item, manCategory);
          } else if (item.id === GUID_ID.COLLECTION_NAV) {
            //render "Collection" navigation
            const collectionList = collection;
            return renderDropDown(item, collectionList);
          } else if (item.id === GUID_ID.SALE_NAV) {
            //render "Sale" navigation
            const saleList = discound;
            return renderDropDown(item, saleList);
          } else if (item.id === GUID_ID.PRODUCT_NAV) {
            //render "Sản phẩm" navigation
            return renderProductNavigationItem(item);
          }

          //render other navigation
          return (
            <li
              key={item.id}
              className={`p-3 flex-none content-center h-full ${
                item.id === "5e09a47e-1559-4277-9a99-99078f68e058"
                  ? "text-red-600"
                  : ""
              }`}
            >
              {item.name}
            </li>
          );
        })}
      </ul>
    </>
  );
};

export default Navigation;
