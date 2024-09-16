import React, { useState } from "react";
import { useDispatch } from "react-redux";
import {
  setCreateCateButton,
  setSelectedCategory,
} from "../../../../redux/slices/admin/categorySlice";
import RemoveWarning from "../../../common/components/modals/RemoveWarning";

const SelectListItem = (props) => {
  const dispatch = useDispatch();
  const { data, level, selectedCategory, isHaveChildren, isShowStepIcon } =
    props;
  const [isDisplayRemoveWarningModal, setIsDisplayRemoveWaringModal] =
    useState(false);

  const handleSelectItem = (e) => {
    let payload = data.find((item) => item.id === e.target.value);
    dispatch(setSelectedCategory(payload));
  };

  const handleClickAddBtn = (e) => {
    let payload = { level: level, isDisplay: true, type: "add" };
    dispatch(setCreateCateButton(payload));
  };

  const handleClickDeleteBtn = (e) => {
    setIsDisplayRemoveWaringModal(true);
  };

  const handleClickEditBtn = (e) => {
    let payload = { level: level, isDisplay: true, type: "edit" };
    dispatch(setCreateCateButton(payload));
  };

  const handleCancelRemoveWarning = () => {
    setIsDisplayRemoveWaringModal(false);
  };

  const renderStepIcon = () => {
    return (
      <div className="w-1/3 content-center text-center">
        <div className="text-black font-medium rounded-full text-sm p-2.5 text-center inline-flex items-cente">
          <svg
            className="w-4 h-4"
            aria-hidden="true"
            xmlns="http://www.w3.org/2000/svg"
            fill="none"
            viewBox="0 0 14 10"
          >
            <path
              stroke="currentColor"
              strokeLinecap="round"
              strokeLinejoin="round"
              strokeWidth={2}
              d="M1 5h12m0 0L9 1m4 4L9 9"
            />
          </svg>
        </div>
      </div>
    );
  };

  const renderEditAndDeleteButtons = () => {
    if (selectedCategory == null) return;
    return (
      <>
        <button
          type="button"
          className="delete-button text-white bg-red-700 hover:bg-red-600 w-7 h-7 rounded-full text-sm mb-2 mx-2"
          onClick={handleClickDeleteBtn}
        >
          <i className="fa-solid fa-trash-can"></i>
        </button>
        <button
          type="button"
          className="edit-button text-white bg-blue-700 hover:bg-blue-600 w-7 h-7 rounded-full text-sm mb-2 mx-2"
          onClick={handleClickEditBtn}
        >
          <i className="fa-regular fa-pen-to-square"></i>
        </button>
      </>
    );
  };
  
  return (
    <div className="cate-selection-item flex">
      <form className="max-w-sm item-list w-2/3">
        <div className="top-title grid grid-cols-2 gap-4 place-content-between items-center">
          <label
            htmlFor={props.title}
            className="block mb-2 text-sm text-gray-900 dark:text-white font-semibold"
          >
            {`Cấp ${props.level}`}
          </label>
          <div className="cate-buttons w-full justify-self-end text-right float-right">
            {renderEditAndDeleteButtons()}
            <button
              type="button"
              className="edit-button text-white bg-blue-700 hover:bg-blue-600 w-7 h-7 rounded-full text-sm mb-2"
              onClick={handleClickAddBtn}
            >
              <i className="fa-solid fa-plus"></i>
            </button>
          </div>
        </div>

        <select
          id={props.title}
          size={props.size ?? 5}
          level={props.level}
          className="no-scrollbar bg-gray-50 border border-gray-300 text-gray-900 text-sm min-h-[142px]
              focus:ring-blue-500 focus:border-blue-500 block w-full p-2.5 
              dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500"
          onChange={handleSelectItem}
        >
          {props.data &&
            props.data.map((item) => {
              return (
                <option key={item.id} value={item.id} className="py-1 flex">
                  {item.name}
                </option>
              );
            })}
        </select>
      </form>
      {isShowStepIcon && renderStepIcon()}
      {isDisplayRemoveWarningModal && (
        <RemoveWarning
          clickCancel={handleCancelRemoveWarning}
          selectedCate={selectedCategory}
          level={level}
          isHaveChildren={isHaveChildren}
        />
      )}
    </div>
  );
};

export default SelectListItem;
