import { yupResolver } from "@hookform/resolvers/yup";
import React, { useState } from "react";
import { useForm } from "react-hook-form";
import * as yup from "yup";
import InputField from "../../../../components/Form/InputField";
import { BUTTON_NAME, BUTTON_TYPE, RESPONSE_API_STATUS, ROLE_NAMES } from "../../../../constants/common";
import { useDispatch, useSelector } from "react-redux";
import { addNewItemData, setAlert, setCreateUpdateUserModal, setUserList, updateItemData } from "../../../../redux/slices/admin/userSlice";
import { Tooltip } from 'flowbite-react';
import { ADMIN_ENDPOINT, PUBLIC_ENDPOINT } from "../../../../constants/endpoint";
import useAxiosBase from "../../../../api/useAxiosBase";
import { ALERT_TYPE, CONFIRM_MODAL_TYPE, STATUS_RESPONSE_HTTP_ENUM } from "../../../../constants/enum";
import useAxiosAuth from "../../../../api/useAxiosAuth";
import ConfirmModal from "../../../../components/Modals/ConfirmModal";
import { USER_FORM_CONST } from "../constants/formConstants";
import { REGEX_PATTERN } from "../../../../constants/regex";

const CreateUpdateUserFormModal = ({ data, type, error, onCreateUser }) => {
  const dispatch = useDispatch()
  const axiosBase = useAxiosBase()
  const axiosAuth = useAxiosAuth()
  const isAddForm = type === BUTTON_TYPE.ADD
  const title = type === BUTTON_TYPE.ADD ? USER_FORM_CONST.TITLES.CREATE : USER_FORM_CONST.TITLES.UPDATE
  const [totalErrorMessage, setTotalErrorMessage] = useState('') 
  const [confirmModal, setconfirmModal] = useState({isDisplay: false, content: ''})
  const {paging, userList } =  useSelector(state => state?.adminUserPage?.data?.userData)
  const searchInputData =  useSelector(state => state?.adminUserPage?.searchInput)
  const roles =  useSelector(state => state?.adminUserPage?.data.contentPageData.roles)
  const labelStyle = "block mb-2 text-sm font-medium text-gray-900 dark:text-white"

  const schema = yup.object({
    userName: yup.string().required(USER_FORM_CONST.VALIDATION_ERRORS.REQUIRED), 
    email: isAddForm ? yup.string().required(USER_FORM_CONST.VALIDATION_ERRORS.REQUIRED) : null,
    roleId:yup.string().required(USER_FORM_CONST.VALIDATION_ERRORS.SELECTION_REQUIRED),
    // phone:yup.string({minLength: {value: 10, message:"Định dạng không đúng"},
    //           validate: (value) => value === "" || /^[0-9]*$/.test(value) || "Định dạng không đúng"}),
    password: isAddForm ? yup.string().required(USER_FORM_CONST.VALIDATION_ERRORS.REQUIRED).min(6, USER_FORM_CONST.VALIDATION_ERRORS.PASSWORD).matches(
      REGEX_PATTERN.passwordWithUpperLowerDigitRegex,
      USER_FORM_CONST.VALIDATION_ERRORS.PASSWORD_RULES
    ) : null,
    repassword: isAddForm ?  yup.string().required(USER_FORM_CONST.VALIDATION_ERRORS.REQUIRED).oneOf([yup.ref('password'), null], USER_FORM_CONST.VALIDATION_ERRORS.PASSWORD_MISMATCH) : null
  }).required();
  
  const originaFormData = {
    userName: !isAddForm ? data?.userData?.userName : '',
    email: !isAddForm ? data?.userData?.email : '',
    roleId: !isAddForm ? data?.userData?.roleId : '',
    phone: !isAddForm ? data?.userData?.phone ?? '' : ''
  }

  const {register , handleSubmit, watch, formState: { errors }} = useForm({
    mode: "onSubmit",
    criteriaMode: "all",
    resolver: yupResolver(schema),
    defaultValues: originaFormData
  });

  const onSubmitForm = (data, e) => {
    e.preventDefault();
    const currentFormValues = watch();
    if (isAddForm) {
      handleCreateUser(currentFormValues)
      return;
    }

    const isChangedForm = JSON.stringify(originaFormData) !== JSON.stringify(currentFormValues)
    if (!isChangedForm) {
      setTotalErrorMessage(USER_FORM_CONST.MESSAGES.NOT_CHANGED)
      return
    }

    handleUpdateUser(currentFormValues)    
  };

  const handleShowResultAlert = (content, type) => {
    dispatch(setAlert({isDisplay:true, content:content, type:type }));

    // Ẩn Alert sau 3 giây
    const timer = setTimeout(() => {
      dispatch(setAlert({isDisplay:false})); 
    }, 3000);

    // Dọn dẹp timeout nếu component cha unmount
    return () => clearTimeout(timer);
  };

  const handleUpdateUser = async (formData) => {
    formData.id = data?.userData?.userId
    try {
      const endpoint = PUBLIC_ENDPOINT.USER_UPDATE;
      const [updateUserResponse] = await Promise.all([
        axiosAuth.post(endpoint, formData ),
      ]);      
      
      if (
        updateUserResponse?.status === STATUS_RESPONSE_HTTP_ENUM.OK &&
        updateUserResponse?.data.status === RESPONSE_API_STATUS.SUCCESS
      ) {
        handleShowResultAlert(USER_FORM_CONST.MESSAGES.UPDATE_SUCCESS(updateUserResponse?.data?.result), ALERT_TYPE.SUCCESS)
        dispatch(setCreateUpdateUserModal({isShow: false}));

        //add roleName
        let roleName = roles.find(x => x.id === formData.roleId)?.roleName
        formData.roleName = roleName
        dispatch(updateItemData(formData));
      }  
    } catch (error) {
      const {status, message} = error?.response?.data
      if (status === RESPONSE_API_STATUS.ERROR) {
        setTotalErrorMessage(message);
        return; 
      }
    }
  };

  const handleCreateUser = async (formData) => {
    formData.roleName = roles.find(x => x.id === formData.roleId)?.roleName
    delete formData.repassword
    try {
      const endpoint = PUBLIC_ENDPOINT.USER_REGISTER;
      const [addUserResponse] = await Promise.all([
        axiosBase.post(endpoint, formData ),
      ]);      
      
      if (
        addUserResponse?.status === STATUS_RESPONSE_HTTP_ENUM.OK &&
        addUserResponse?.data.status === RESPONSE_API_STATUS.SUCCESS
      ) {
        //add userId
        formData.userId = addUserResponse?.data?.result?.userId;

        dispatch(setCreateUpdateUserModal({isShow: false}));
        dispatch(addNewItemData({newItem: formData, isRemoveLastItem: userList?.length === paging.pageSize}))
        handleShowResultAlert(USER_FORM_CONST.MESSAGES.CREATE_SUCCESS, ALERT_TYPE.SUCCESS)

      }  
    } catch (error) {
      const {status, message} = error.response?.data
      if (status === RESPONSE_API_STATUS.ERROR) {
        setTotalErrorMessage(message);
        return;
      }
    }
  };

  const handleClickDeleteBtn = () => {
    const modalContent = USER_FORM_CONST.MESSAGES.DELETE_CONFIRM(data?.userData?.userName)
    setconfirmModal({isDisplay: true, content:modalContent})
  };

  const handleDeleteUser = async () => {
    try {
      const endpoint = ADMIN_ENDPOINT.USER_DELETE;
      const params = {
        userId: data?.userData?.userId,
        pageNumber: paging.pageNumber,
        pageSize: paging.pageSize,
        sortby: paging.sortby,
        sortOrder: paging.sortOrder ?? null,
        searchInput: searchInputData ?? null
      }

      const [deleteUserResponse] = await Promise.all([
        axiosAuth.post(endpoint, params),
      ]);      
      
      if (
        deleteUserResponse?.status === STATUS_RESPONSE_HTTP_ENUM.OK &&
        deleteUserResponse?.data.status === RESPONSE_API_STATUS.SUCCESS
      ) {
        handleShowResultAlert( USER_FORM_CONST.MESSAGES.DELETE_CONFIRM(deleteUserResponse?.data?.result.userName), ALERT_TYPE.SUCCESS)
        dispatch(setCreateUpdateUserModal({isShow: false}));
        dispatch(setUserList(deleteUserResponse?.data?.result.pagingUserList.userList))
      }  
    } catch (error) {
      const {status, message} = error?.response?.data
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
          onClick={() => dispatch(setCreateUpdateUserModal({isShow: false}))}
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
      <Tooltip content={message}placement="top">
        <i className="fa-solid fa-circle-exclamation text-red-500"></i>
      </Tooltip>
    );
  };

  const renderPassInput = () => {
    if (!isAddForm) return null
    return (
      <>
        <div className="sm:col-span-2">
          <InputField
            type="password"
            label={USER_FORM_CONST.LABELS.PASSWORD}
            labelStyle={labelStyle}
            name="password"
            register={register}
            errors={errors}
            className="bg-gray-50 border border-gray-300 text-gray-900  text-sm rounded-lg focus:ring-primary-600 focus:border-primary-600 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-primary-500 dark:focus:border-primary-500"
          />
        </div>
        <div className="sm:col-span-2">
          <InputField
            type="password"
            label={USER_FORM_CONST.LABELS.REPASSWORD}
            labelStyle={labelStyle}
            name="repassword"
            register={register}
            errors={errors}
            className="bg-gray-50 border border-gray-300 text-gray-900  text-sm rounded-lg focus:ring-primary-600 focus:border-primary-600 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-primary-500 dark:focus:border-primary-500"
          />
        </div>
      </>
    );
  }

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
                  label={USER_FORM_CONST.LABELS.USERNAME}
                  labelStyle={labelStyle}
                  name="userName"
                  register={register}
                  errors={errors}
                  className="bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-primary-600 focus:border-primary-600 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-primary-500 dark:focus:border-primary-500"
                />
              </div>
              <div className="sm:col-span-2">
                <InputField
                  label={USER_FORM_CONST.LABELS.EMAIL}
                  labelStyle={labelStyle}
                  name="email"
                  register={register}
                  errors={errors}
                  disabled={!isAddForm}
                  className={`bg-gray-50 border border-gray-300 text-gray-900  text-sm rounded-lg focus:ring-primary-600
                  focus:border-primary-600 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600
                   dark:placeholder-gray-400 dark:text-white dark:focus:ring-primary-500 dark:focus:border-primary-500
                   ${!isAddForm ? 'bg-slate-300' : ''}`}
                />
              </div>
              {renderPassInput()}
              <div>
                <label
                  htmlFor="role"
                  className="flex mb-2 text-sm font-medium text-gray-900 dark:text-white"
                >
                  <p className="mr-2">{USER_FORM_CONST.LABELS.ROLE}</p>
                  {errors["roleId"] && renderValidateErrorTooltip(errors["roleId"].message)}
                </label>
                <select
                  {...register("roleId")}
                  id="role"
                  className="bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-primary-500 focus:border-primary-500 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-primary-500 dark:focus:border-primary-500"
                >
                  <option key="999" disabled hidden value="">
                    {USER_FORM_CONST.LABELS.OPTIONS}
                  </option>
                  {roles?.map((item) => {
                    if (item.roleName === ROLE_NAMES.CUSTOMER) return null;
                    return (
                      <option key={item.id} value={item.id}>
                        {item.roleName}
                      </option>
                    );
                  })}
                </select>
              </div>
            </div>
          </div>
          {renderButtons()}
        </form>
      </div>
      {confirmModal.isDisplay && 
        <ConfirmModal 
          content={confirmModal.content} 
          type={CONFIRM_MODAL_TYPE.DELETE}
          onClickConfirm={handleDeleteUser}
          onClickCancel={() => setconfirmModal({isDisplay: false})}/> 
      }
    </section>
  );
};

export default CreateUpdateUserFormModal;
