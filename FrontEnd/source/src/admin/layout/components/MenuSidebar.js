import React, { useState } from "react";
import { Link } from "react-router-dom";
import { getFirstCharsByLengthNumber } from "../../common/utils/stringUtil";

const MenuSidebar = ({data}) => {
  const createId = getFirstCharsByLengthNumber(data.id, 8);
  const dataAccordionTargetId = `accordion-open-body-${createId}`;
  const accordionHeadingId = `accordion-open-heading-${createId}`;
  const [isExpanded, setIsExxpanded] = useState(false);   

  const renderDropdownIcon = () => {
    return (
      <>
        <svg
          data-accordion-icon=""
          className="w-3 h-3 rotate-180 shrink-0 ml-1"
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

  const renderSubMenuSidebar = (data) => {
    return (
      <div className="sub-menu translate transform overflow-hidden block">
        <ul className="my-3 flex flex-col gap-2.5 pl-6">
          {data.map((item) => {
            return (
              <li key={item.id}>
                <Link
                  to={item.path}
                  className="group relative flex items-center gap-2.5 rounded-md px-4 font-medium
                   duration-300 ease-in-out text-white hover:text-gray-400"
                >
                  {item.name}
                </Link>
              </li>
            );
          })}
        </ul>
      </div>
    );
  };

  const handleMouseOutMenuItem = (e) => {
    //fix  lỗi background trắng khi mouse out
    e.target.classList.remove("bg-gray-100", "dark:bg-gray-800");
  };

  const hanldeClickMenu = () => {
    console.log('hanldeClickMenu')
    setIsExxpanded(!isExpanded)
  };

  return (
    <div className="menu-item" key={data.id}>
      <h2 id={accordionHeadingId}>
        <button
          type="button"
          className="flex items-center justify-between w-full p-3 font-medium rtl:text-right text-gray-100 dark:border-gray-700 dark:text-gray-400
          hover:bg-gray-700 dark:hover:bg-gray-800 gap-3 bg-black"
          data-accordion-target={`#${dataAccordionTargetId}`}
          aria-expanded={isExpanded}
          aria-controls={dataAccordionTargetId}
          onMouseOut={handleMouseOutMenuItem}
          onClick={hanldeClickMenu}
        >
          <span className="flex items-center w-full place-content-between">
            <i className={`fa fa-${data.icon} mr-2`} />
            {data.name}
            {data.children && renderDropdownIcon()}
          </span>
        </button>
      </h2>
      <div
        id={dataAccordionTargetId}
        className={isExpanded ? "" : "hidden"}
        aria-labelledby={accordionHeadingId}
      >
        {data.children && renderSubMenuSidebar(data.children)}
      </div>
    </div>
  );
};

export default MenuSidebar;
