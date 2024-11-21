import { yupResolver } from "@hookform/resolvers/yup";
import React, { useState } from "react";
import { useForm } from "react-hook-form";
import * as yup from "yup";
import InputField from "../../../../components/Form/InputField";
import {
  BUTTON_NAME,
  BUTTON_TYPE,
  RESPONSE_API_STATUS,
} from "../../../../constants/common";
import { useDispatch, useSelector } from "react-redux";
import {addNewItemData, setCreateUpdateCollectionModal,
  setAlert, updateCollection,deleteCollection } from "../../../../redux/slices/admin/collectionSlice";
import { Tooltip } from "flowbite-react";
import {
  ADMIN_ENDPOINT,
} from "../../../../constants/endpoint";
import {
  ALERT_TYPE,
  CONFIRM_MODAL_TYPE,
  STATUS_RESPONSE_HTTP_ENUM,
} from "../../../../constants/enum";
import useAxiosAuth from "../../../../api/useAxiosAuth";
import ConfirmModal from "../../../../components/Modals/ConfirmModal";
import { COLLECTION_FORM_CONST } from "../constants/formConstants";
import { REGEX_PATTERN } from "../../../../constants/regex";

const CreateUpdateCollectionFormModal = ({ data, type}) => {
  const dispatch = useDispatch();
  const axiosAuth = useAxiosAuth();
  const isAddForm = type === BUTTON_TYPE.ADD;
  const title =
    type === BUTTON_TYPE.ADD
      ? COLLECTION_FORM_CONST.TITLES.CREATE
      : COLLECTION_FORM_CONST.TITLES.UPDATE;
  const [totalErrorMessage, setTotalErrorMessage] = useState("");
  const [confirmModal, setconfirmModal] = useState({
    isDisplay: false,
    content: "",
  });
  const {paging, collectionData}  = useSelector(
    (state) => state?.adminCollection?.data?.collectionData
  );
  const searchInputData = useSelector(
    (state) => state?.adminUserPage?.searchInput
  );
  const labelStyle = "block mb-2 text-sm font-medium text-gray-900 dark:text-white";
  const schema = yup.object({
      name: yup.string().required(COLLECTION_FORM_CONST.VALIDATION_ERRORS.REQUIRED),
      description: yup.string()
    }).required();

  const originaFormData = {
    name: !isAddForm ? data?.collectionData?.name : "",
    description: !isAddForm ? data?.collectionData?.description : "",
  };

  const { register, handleSubmit, watch, formState: { errors }} = useForm({
    mode: "onSubmit",
    criteriaMode: "all",
    resolver: yupResolver(schema),
    defaultValues: originaFormData,
  });

  const onSubmitForm = (data, e) => {
    e.preventDefault();
    const currentFormValues = watch();
    if (isAddForm) {
      handleCreateCollection(currentFormValues);
      return;
    }

    const isChangedForm =
      JSON.stringify(originaFormData) !== JSON.stringify(currentFormValues);
    if (!isChangedForm) {
      setTotalErrorMessage(COLLECTION_FORM_CONST.MESSAGES.NOT_CHANGED);
      return;
    }

    handleEditColleciton(currentFormValues);
  };

  const handleShowResultAlert = (content, type) => {
    dispatch(setAlert({ isDisplay: true, content: content, type: type }));

    // Ẩn Alert sau 3 giây
    const timer = setTimeout(() => {
      dispatch(setAlert({ isDisplay: false }));
    }, 3000);

    // Dọn dẹp timeout nếu component cha unmount
    return () => clearTimeout(timer);
  };

  const handleEditColleciton = async (formData) => {
    formData.id = data?.collectionData.id
    try {
      const endpoint = ADMIN_ENDPOINT.COLLECTION_EDIT;
      const [editCollectionResponse] = await Promise.all([
        axiosAuth.post(endpoint, formData),
      ]);

      if (
        editCollectionResponse?.status === STATUS_RESPONSE_HTTP_ENUM.OK &&
        editCollectionResponse?.data.status === RESPONSE_API_STATUS.SUCCESS
      ) {
        dispatch(updateCollection(formData));
        handleShowResultAlert(
          COLLECTION_FORM_CONST.MESSAGES.UPDATE_SUCCESS(
            editCollectionResponse?.data?.result
          ),
          ALERT_TYPE.SUCCESS
        );
        dispatch(setCreateUpdateCollectionModal({ isShow: false }));
      }
    } catch (error) {
      const { status, message } = error?.response?.data;
      if (status === RESPONSE_API_STATUS.ERROR) {
        setTotalErrorMessage(message);
        return;
      }
    }
  };

  const handleCreateCollection = async (formData) => {
    try {
      const endpoint = ADMIN_ENDPOINT.COLLECTION_ADD;
      const [addCollectionResponse] = await Promise.all([
        axiosAuth.post(endpoint,formData),
      ]);

      if (
        addCollectionResponse?.status === STATUS_RESPONSE_HTTP_ENUM.OK &&
        addCollectionResponse?.data.status === RESPONSE_API_STATUS.SUCCESS
      ) {

        dispatch(addNewItemData({newItem: formData}))
        // dispatch(addNewItemData({newItem: formData, isRemoveLastItem: userList?.length === paging.pageSize}))
        handleShowResultAlert(
          COLLECTION_FORM_CONST.MESSAGES.CREATE_SUCCESS,
          ALERT_TYPE.SUCCESS
        );
        dispatch(setCreateUpdateCollectionModal({ isShow: false }))
      }
    } catch (error) {
      const { status, message } = error.response?.data;
      if (status === RESPONSE_API_STATUS.ERROR) {
        setTotalErrorMessage(message);
        return;
      }
    }
  };
  const handleClickDeleteBtn = () => {
    const modalContent = COLLECTION_FORM_CONST.MESSAGES.DELETE_CONFIRM(
      data?.collectionData?.name
    );
    setconfirmModal({ isDisplay: true, content: modalContent });
  };

  const handleDeleteUser = async () => {
    try {
      const endpoint = ADMIN_ENDPOINT.COLLECTION_DELETE;
      const params = {
        Id: data?.collectionData?.id,
        pageNumber: paging.pageNumber,
        pageSize: paging.pageSize,
        sortby: paging.sortby,
        sortOrder: paging.sortOrder ?? null,
        searchInput: searchInputData ?? null,
      };

      const [deleteUserResponse] = await Promise.all([
        axiosAuth.post(endpoint, params),
      ]);

      if (
        deleteUserResponse?.status === STATUS_RESPONSE_HTTP_ENUM.OK &&
        deleteUserResponse?.data.status === RESPONSE_API_STATUS.SUCCESS
      ) {
        handleShowResultAlert(
          COLLECTION_FORM_CONST.MESSAGES.DELETE_SUCCESS(data?.collectionData?.name),
          ALERT_TYPE.SUCCESS
        );
        dispatch(setCreateUpdateCollectionModal({ isShow: false }));
        dispatch(deleteCollection(deleteUserResponse?.data?.result.pagingCollectionList.collectionList)
        );
      }
    } catch (error) {
      const { status, message } = error?.response?.data;
      if (status === RESPONSE_API_STATUS.ERROR) {
        setTotalErrorMessage(message);
        return;
      }
    }
  };

  const renderButtons = () => {
    return (
      <div className="flex items-center justify-between w-full">
        <button
          type="submit"
          className="text-blue-700 hover:text-white border border-blue-700 hover:bg-blue-800 focus:ring-4 focus:outline-none focus:ring-blue-300
         font-medium rounded-lg text-sm px-5 py-2.5 text-center dark:border-blue-500 dark:text-blue-500
          dark:hover:text-white dark:hover:bg-blue-500 dark:focus:ring-blue-800"
        >
          {!isAddForm ? BUTTON_NAME.UPDATE : BUTTON_NAME.ADD}
        </button>
        <button
          type="button"
          className="text-gray-900 hover:text-white border border-gray-800 hover:bg-gray-900 focus:ring-4 focus:outline-none
         focus:ring-gray-300 font-medium rounded-lg text-sm px-5 py-2.5 text-center ml-2 dark:border-gray-600 dark:text-gray-400
          dark:hover:text-white dark:hover:bg-gray-600 dark:focus:ring-gray-800"
          onClick={() =>
            dispatch(setCreateUpdateCollectionModal({ isShow: false }))
          }
        >
          Hủy
        </button>
        <button
          type="button"
          onClick={handleClickDeleteBtn}
          className="text-red-600 ml-auto inline-flex items-center hover:text-white border
           border-red-600 hover:bg-red-600 focus:ring-4 focus:outline-none focus:ring-red-300
            font-medium rounded-lg text-sm px-5 py-2.5 text-center dark:border-red-500 dark:text-red-500
             dark:hover:text-white dark:hover:bg-red-600 dark:focus:ring-red-900"
        >
          <i className="fa-solid fa-trash-can mr-2"></i>
          Xóa
        </button>
      </div>
    );
  };

  const renderValidateErrorTooltip = (message) => {
    return (
      <Tooltip content={message} placement="top">
        <i className="fa-solid fa-circle-exclamation text-red-500"></i>
      </Tooltip>
    );
  };

  return (
    <section className="fixed inset-0 flex items-center justify-center z-[300] bg-opacity-75 bg-gray-300">
      <div className="px-4 py-8 mx-auto lg:py-12 bg-white w-1/4 rounded-lg">
        <h2 className="block text-xl font-bold text-gray-900 dark:text-white text-center">
          <p>{title}</p>
          <p
            className={`h-4 text-red-500 text-xs ${
              !totalErrorMessage ? "invisible" : ""
            }`}
          >
            {totalErrorMessage}
          </p>
        </h2>
        <form onSubmit={handleSubmit(onSubmitForm)}>
          <div className="grid gap-4 mb-4 px-4 sm:gap-6 sm:mb-5 max-h-[50vh] overflow-y-auto scrollbar-custom">
            <div className="w-full space-y-5 py-4">
              <div className="sm:col-span-2">
                <InputField
                  label={COLLECTION_FORM_CONST.LABELS.USERNAME}
                  labelStyle={labelStyle}
                  name="name"
                  register={register}
                  errors={errors}
                  className="bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-primary-600 focus:border-primary-600 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-primary-500 dark:focus:border-primary-500"
                />
              </div>
              <div className="sm:col-span-2">
                <label
                  htmlFor="description"
                  className="block mb-2 text-sm font-medium text-gray-900 dark:text-white"
                >
                  Mô Tả
                </label>
                <textarea
                  {...register("description")}
                  id="description"
                  rows={4}
                  className="block p-2.5 w-full text-sm text-gray-900 bg-gray-50 rounded-lg border border-gray-300 focus:ring-blue-500 focus:border-blue-500 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500"
                  placeholder="Mô tả chi tiết"
                  defaultValue=""
                />
              </div>
            </div>
          </div>
          {renderButtons()}
        </form>
      </div>
      {confirmModal.isDisplay && (
        <ConfirmModal
          content={confirmModal.content}
          type={CONFIRM_MODAL_TYPE.DELETE}
          onClickConfirm={handleDeleteUser}
          onClickCancel={() => setconfirmModal({ isDisplay: false })}
        />
      )}
    </section>
  );
};

export default CreateUpdateCollectionFormModal;
