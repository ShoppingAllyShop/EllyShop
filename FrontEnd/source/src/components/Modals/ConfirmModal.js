import React from "react";
import { CONFIRM_MODAL_TYPE } from "../../constants/enum";

const ConfirmModal = ({ content, type, onClickConfirm, onClickCancel }) => {

    let buttonColor;

    // Dùng switch case để đặt className dựa trên giá trị của status
    switch (type) {
      case CONFIRM_MODAL_TYPE.DELETE:
        buttonColor =
          "bg-red-600 hover:bg-red-800 dark:focus:ring-red-800 focus:ring-red-300";
        break;
      case CONFIRM_MODAL_TYPE.NORMAL:
        buttonColor =
          "bg-blue-700 hover:bg-blue-800 dark:focus:ring-blue-800 focus:ring-blue-300";
        break;
      default:
        buttonColor =
          "bg-red-600 hover:bg-red-800 dark:focus:ring-red-800 focus:ring-red-300";
        break;
    }

  return (
    <div
      id="confirm-modal"
      className="fixed inset-0 flex items-center justify-center z-[300]w-full md:inset-0 bg-gray-800 bg-opacity-50"
    >
      <div className="relative p-4 w-full max-w-md max-h-full">
        <div className="relative bg-white rounded-lg shadow dark:bg-gray-700">
          <button
            type="button"
            className="absolute top-3 end-2.5 text-gray-400 bg-transparent hover:bg-gray-200 hover:text-gray-900 rounded-lg text-sm w-8 h-8 ms-auto inline-flex justify-center items-center dark:hover:bg-gray-600 dark:hover:text-white"
            data-modal-hide="popup-modal"
          >
            <i className="fa-solid fa-xmark"></i>
            <span className="sr-only">Close modal</span>
          </button>
          <div className="p-4 md:p-5 text-center">
            <i className="fa-solid fa-circle-exclamation text-3xl text-gray-900 mb-3"></i>
            <h3 className="mb-5 text-lg font-normal text-gray-500 dark:text-gray-600">
              {content}
            </h3>
            <button
              data-modal-hide="popup-modal"
              type="button"
              className={`text-white focus:ring-4 focus:outline-none font-medium rounded-lg text-sm inline-flex 
              items-center px-5 py-2.5 text-center ${buttonColor}`}
              onClick={onClickConfirm}
            >
              Chắc chắn
            </button>
            <button
              onClick={onClickCancel}
              data-modal-hide="popup-modal"
              type="button"
              className="py-2.5 px-5 ms-3 text-sm font-medium text-gray-900 focus:outline-none bg-white rounded-lg border border-gray-200 hover:bg-gray-100 hover:text-blue-700 focus:z-10 focus:ring-4 focus:ring-gray-100 dark:focus:ring-gray-700 dark:bg-gray-800 dark:text-gray-400 dark:border-gray-600 dark:hover:text-white dark:hover:bg-gray-700"
            >
              Hủy
            </button>
          </div>
        </div>
      </div>
    </div>
  );
};

export default ConfirmModal;
