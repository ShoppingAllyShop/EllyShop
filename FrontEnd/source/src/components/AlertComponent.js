import React from "react";
import { ALERT_TYPE } from "../constants/enum";

const AlertComponent = ({content, type, isShow}) => {
  let classNameDiv = ""
  let iconClassName = ""
  let idName = ""
  let alertBackgroundColor = ""
  
  const initData = type => {
    switch (type) {
      case ALERT_TYPE.SUCCESS:
        idName = "alert-success"
        classNameDiv = "text-green-500 bg-green-100 dark:bg-green-800 dark:text-green-200";
        iconClassName = "fa-regular fa-circle-check";
        alertBackgroundColor = "bg-green-100"
        break;
      case ALERT_TYPE.ERROR:
        idName = "alert-danger"
        classNameDiv = "text-red-500 bg-red-100 dark:bg-red-800 dark:text-red-200";
        iconClassName = "fa-solid fa-circle-exclamation";
        alertBackgroundColor = "bg-red-100"
        break;
      case ALERT_TYPE.WARNING:
        idName = "alert-warning"
        classNameDiv = "text-orange-500 bg-orange-100 dark:bg-orange-700 dark:text-orange-200";
        iconClassName = "fa-solid fa-triangle-exclamation";
        alertBackgroundColor = "bg-orange-100"
        break;
      default:
        break;
    }
  }
  initData(type)
  
  return (
    <div
      id={idName}
      className={`${alertBackgroundColor} z-[600] inline-flex items-center w-fit p-4 mb-4
        absolute top-0 left-1/2 -translate-x-1/2 translate-y-2
       text-gray-500 rounded-lg shadow dark:text-gray-400 dark:bg-gray-800 
       transform transition-transform duration-500 ease-in-out 
       ${isShow ? 'translate-y-0 opacity-100' : '-translate-y-10 opacity-0'}`}
      role="alert"
    >
      <div className={`inline-flex items-center justify-center flex-shrink-0 w-8 h-8 rounded-lg ${classNameDiv}`}>
        <i className={iconClassName}></i>
        <span className="sr-only">Check icon</span>
      </div>
      <div className="ms-3 text-sm font-normal text-gray-600">{content}</div>
      <button
        type="button"
        className="inline-flex items-center justify-center h-8 ml-2 -my-1.5 
                  text-gray-400 hover:text-gray-900 rounded-lg focus:ring-2 focus:ring-gray-300 p-1.5 
                  dark:text-gray-500 dark:hover:text-white dark:bg-gray-800 dark:hover:bg-gray-700"
        data-dismiss-target={`#${idName}`}
        aria-label="Close"
      >
        <span className="sr-only">Close</span>
        <i className="fa-solid fa-xmark"></i>
      </button>
    </div>
  );
};

export default AlertComponent;
