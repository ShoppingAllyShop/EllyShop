import React, { useState } from "react";
import { useDispatch } from "react-redux";
import {
  deleteCategory,
  setAlert,
  setCreateCateButton,
  setSelectedCategory,
  triggerEditModal,
} from "../../../../redux/slices/admin/categorySlice";
import RemoveWarning from "../../../common/components/modals/RemoveWarning";
import useAxiosAuth from "../../../../api/useAxiosAuth";
import { ADMIN_ENDPOINT } from "../../../../constants/endpoint";
import { BUTTON_TYPE, RESPONSE_API_STATUS } from "../../../../constants/common";
import { ALERT_TYPE } from "../../../../constants/enum";

const SelectListItem = (props) => {
  const dispatch = useDispatch();
  const auth = useAxiosAuth()
  const { data, level, selectedCategory, isHaveChildren, isShowStepIcon } =
    props;
  const [isDisplayRemoveWarningModal, setIsDisplayRemoveWaringModal] =
    useState(false);

  const handleSelectItem = (e) => {
    let payload = data.find((item) => item.id === e.target.value);
    dispatch(setSelectedCategory(payload));
  };

  const handleClickAddBtn = (e) => {
    let payload = { level: level, isDisplay: true, type: BUTTON_TYPE.ADD };
    dispatch(setCreateCateButton(payload));
  };

  const handleClickDeleteBtn = (e) => {
    setIsDisplayRemoveWaringModal(true);
  };

  const handleClickEditBtn = (e) => {
    let payload = { level: level, isDisplay: true, type: BUTTON_TYPE.EDIT };
    dispatch(setCreateCateButton(payload));
  };

  const handleCancelRemoveWarning = () => {
    setIsDisplayRemoveWaringModal(false);
  };

  const handleAgreeDelete = async (category) => {
    const cate = {
      id: category.id,
      name: category.name
    }

    //Call login api
    const endpoint = ADMIN_ENDPOINT.CATEGORY_DELETE;
    const [deleteResponse] = await Promise.all([
      auth.post(endpoint, cate)
    ]);

    if (deleteResponse?.data?.status === RESPONSE_API_STATUS.SUCCESS) {
      handleShowResult("Bạn đã xóa danh mục thành công", ALERT_TYPE.SUCCESS)

       //add to store
      dispatch(deleteCategory(cate));
      setIsDisplayRemoveWaringModal(false)
    }
  };

  const handleShowResult = (content, type) => {
    console.log('handleShowResult')
    dispatch(setAlert({isDisplay:true, content:content, type:type }));
    dispatch(triggerEditModal({isDisplay:false}))
    
    // Ẩn Alert sau 3 giây
    const timer = setTimeout(() => {
      dispatch(setAlert({isDisplay:false})); 
    }, 3000);

    // Dọn dẹp timeout nếu component cha unmount
    return () => clearTimeout(timer);
  };
  const renderStepIcon = () => {
    return (
      <div className="w-full lg:w-1/3 content-center text-center">
        <div className="text-black font-medium rounded-full text-sm p-2.5 text-center inline-flex items-cente">
          <svg
            className="w-4 h-4 rotate-90 lg:rotate-0"
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
          className="delete-button text-white bg-red-700 hover:bg-red-600 w-7 h-7 rounded-full text-sm mb-2"
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
    <div className="cate-selection-item mb-5 lg:flex lg:mb-0">
      <form className="item-list w-full mx-auto lg:w-2/3 sm:w-4/5 lg:mx-0">
        <div className="top-title flex place-content-between items-center">
          <label
            htmlFor={props.title}
            className="block mb-2 text-sm text-gray-900 dark:text-white font-semibold whitespace-nowrap"
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
          agreeDelete={(data) => handleAgreeDelete(data)}
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
