import React from "react";
import DangerAlert from "../alerts/DangerAlert";

const RemoveWarning = ({
  clickCancel,
  selectedCate,
  isHaveChildren,
  level,
}) => {
  return (
    <div
      tabIndex={-1}
      className="fixed inset-0 flex items-center justify-center z-50 bg-opacity-75 bg-gray-300"
    >
      <div className="relative p-4 w-full max-w-md max-h-full">
        <div className="relative bg-white rounded-lg shadow dark:bg-gray-700">
          <button
            type="button"
            className="absolute top-3 end-2.5 text-gray-400 bg-transparent 
            hover:bg-gray-200 hover:text-gray-900 rounded-lg text-sm w-8 h-8 ms-auto
             inline-flex justify-center items-center dark:hover:bg-gray-600 dark:hover:text-white"
            onClick={clickCancel}
          >
            <svg
              className="w-3 h-3"
              xmlns="http://www.w3.org/2000/svg"
              fill="none"
              viewBox="0 0 14 14"
            >
              <path
                stroke="currentColor"
                strokeLinecap="round"
                strokeLinejoin="round"
                strokeWidth={2}
                d="m1 1 6 6m0 0 6 6M7 7l6-6M7 7l-6 6"
              />
            </svg>
            <span className="sr-only">Close modal</span>
          </button>
          <div className="p-4 md:p-5 text-center">
            <h2 className="font-semibold mb-2">Xóa danh mục sản phẩm</h2>
            <h3 className="mb-5 text-lg font-normal text-gray-500 dark:text-gray-400">
              {isHaveChildren && (
                <DangerAlert
                  content={`Vui lòng xóa danh mục cấp ${level + 1} trước`}
                />
              )}
              {!isHaveChildren &&
                `Bạn có chắc muốn xóa "${selectedCate.name}" ?`}
            </h3>
            <button
              type="button"
              onClick={clickCancel}
              className="text-white bg-gray-600 hover:bg-gray-800 focus:ring-4 
              focus:outline-none focus:ring-red-300 dark:focus:ring-gray-800 
              font-medium rounded-lg text-sm inline-flex items-center px-5 py-2.5 text-center"
            >
              {isHaveChildren ? "Đóng" : "Đồng ý"}
            </button>
            {!isHaveChildren && (
              <button
                type="button"
                className="py-2.5 px-5 ms-3 text-sm font-medium text-gray-900 focus:outline-none bg-white rounded-lg border
                 border-gray-200 hover:bg-gray-100 hover:text-blue-700 focus:z-10 focus:ring-4 focus:ring-gray-100
                  dark:focus:ring-gray-700 dark:bg-gray-800 dark:text-gray-400 dark:border-gray-600 dark:hover:text-white
                   dark:hover:bg-gray-700"
                onClick={clickCancel}
              >
                Hủy
              </button>
            )}
          </div>
        </div>
      </div>
    </div>
  );
};

export default RemoveWarning;
