import { useGoogleLogin } from "@react-oauth/google";
import React, { useState } from "react";
import { Link, useNavigate } from "react-router-dom";
import { checkIsAdminPage } from "../../../utils/httpUtil";
import googleIcon from "../../../images/icons/google.png";
import facebookIcon from "../../../images/icons/facebook1.png";
import FacebookLogin from "react-facebook-login/dist/facebook-login-render-props";
import { capitalizeFirstLetter } from "../../../utils/stringUtil";
import {
  FACEBOOK_LOGIN_CONST,
  PAGE_TYPE,
  RESPONSE_API_STATUS,
  STORAGE_TYPE,
  TEXT_CONSTANTS,
  USER_LOCAL_STORAGE_KEY,
} from "../../../constants/common";
import { AUTH_PROVIDERS } from "../../../constants/common";
import useAxiosBase from "../../../api/useAxiosBase";
import storageUtil from "../../../utils/storageUtil";
import { useForm } from "react-hook-form";
import { yupResolver } from "@hookform/resolvers/yup";
import * as yup from "yup";
import InputField from "../../../components/Form/InputField";
import { useDispatch } from "react-redux";
import { setUser } from "../../../redux/slices/admin/adminLayoutSlice";
import { ADMIN_ENDPOINT, PUBLIC_ENDPOINT } from "../../../constants/endpoint";
import Loading from "../../../components/Loading";
import { LOGIN_FORM_CONST } from "./constants/formConstants";

const LoginPage = () => {
  const navigate = useNavigate();
  const axiosBase = useAxiosBase();
  const dispatch = useDispatch();
  const [loginError, setLoginError] = useState("");
  const [isLoading, setIsLoading] = useState(false)

  const schema = yup
    .object({
      email: yup
        .string()
        .required(LOGIN_FORM_CONST.VALIDATION_ERRORS.REQUIRED)
        .email(LOGIN_FORM_CONST.VALIDATION_ERRORS.REQUIRED_EMAIL),
      password: yup.string().required(LOGIN_FORM_CONST.VALIDATION_ERRORS.REQUIRED),
    })
    .required();

  const {
    register,
    handleSubmit,
    formState: { errors },
  } = useForm({
    mode: "onSubmit",
    criteriaMode: "all",
    resolver: yupResolver(schema),
  });

  const googleResponseMessage = async (response) => {
    const params = {
      token: response.access_token,
      pageType: checkIsAdminPage() ? PAGE_TYPE.ADMIN_PAGE : PAGE_TYPE.CLIENT_PAGE,
      provider: AUTH_PROVIDERS.GOOGLE,
    };

    const endpoint = PUBLIC_ENDPOINT.SOCIAL_LOGIN;
    const [loginResponse] = await Promise.all([
      axiosBase.post(endpoint, params),
    ]);

    hanldeLoginResponse(loginResponse);
  };

  const googleErrorMessage = (error) => {
    return;
  };

  const googleLogin = useGoogleLogin({
    onSuccess: googleResponseMessage,
    onError: googleErrorMessage,
  });

  const handleFacebookCallback = async (facebookInfo) => {
    if (facebookInfo?.status === "unknown") {
      console.error("Sorry!", "Something went wrong with facebook Login.");
      return;
    }
    const params = {
      token: facebookInfo.accessToken,
      provider: AUTH_PROVIDERS.FACEBOOK,
      isCustomer: false,
    };

    //Call login api
    const endpoint = PUBLIC_ENDPOINT.SOCIAL_LOGIN;
    const [loginResponse] = await Promise.all([
      axiosBase.post(endpoint, params),
      setIsLoading(true)
    ]);

    hanldeLoginResponse(loginResponse);
    setIsLoading(false)
  };

  const onSubmitLoginForm = async (data) => {
    const params = {
      email: data.email,
      password: data.password,
      isCustomer: false,
    };

    //Call login api
    try {
      const endpoint = PUBLIC_ENDPOINT.LOGIN;
      const [loginResponse] = await Promise.all([
        axiosBase.post(endpoint, params),
        setIsLoading(true)
      ]);
      hanldeLoginResponse(loginResponse);
    } catch (error) {
       hanldeLoginResponse(error.response);  
    }
    setIsLoading(false)   
  };

  const hanldeLoginResponse = (loginResponse) => {
    //Display error if login failed
    const {status, message} = loginResponse.data
    if (status === RESPONSE_API_STATUS.ERROR) {
      setLoginError(message);
      return;
    }
    const { email, role, userName, accessToken, refreshToken } =
      loginResponse.data.result;

    //Set session storage
    storageUtil.setItem(
      USER_LOCAL_STORAGE_KEY,
      { accessToken: accessToken, refreshToken: refreshToken, roleName: role.roleName },
      STORAGE_TYPE.SESSION
    );

    //dispatch store
    dispatch(setUser({ email: email, role: role, userName: userName }));

    //Redirect to mainboard
    navigate(`/${ADMIN_ENDPOINT.DASHBOARD}`);
  };

  const renderSocialLogin = (type, provider = null) => {
    let loginFunc = null;
    let icon = null;

    switch (type) {
      case TEXT_CONSTANTS.GOOGLE:
        loginFunc = googleLogin;
        icon = googleIcon;
        break;
      case TEXT_CONSTANTS.FACEBOOK:
        icon = facebookIcon;
        break;
      default:
        break;
    }

    return (
      <button
        onClick={ type !== TEXT_CONSTANTS.FACEBOOK ? loginFunc : provider.onClick}
        className="flex h-full w-full bg-white border border-gray-300 px-3 items-center
      text-[12px] font-medium text-gray-800 hover:bg-gray-100"
      >
        <img src={icon} alt="" className="size-7" />
        <span className="ml-1">Login with {capitalizeFirstLetter(type)}</span>
      </button>
    );
  };

  const renderLoginForm = () => {
    const inputStyle = "text-gray-700 border border-gray-300 rounded py-2 px-4 block w-full focus:outline-2 focus:outline-blue-700"
    return (
      <form onSubmit={handleSubmit(onSubmitLoginForm)}>
        <div className="mt-4">
          <InputField
            label={LOGIN_FORM_CONST.LABELS.EMAIL}
            name="email"
            register={register}
            errors={errors}
            className={inputStyle}
          />
        </div>
        <div className="mt-4 flex flex-col justify-between">
          <InputField
            label={LOGIN_FORM_CONST.LABELS.PASSWORD}
            name="password"
            type="password"
            register={register}
            errors={errors}
            className={inputStyle}
          />
          <Link href="#" className="text-xs text-gray-500 hover:text-gray-900 text-end w-full mt-2">
            {LOGIN_FORM_CONST.LABELS.FORGET_PASSWORD}
          </Link>
        </div>
        <div className="mt-8">
          <button
            type="submit"
            className="inline-flex items-center justify-center gap-2.5 rounded-md bg-blue-600 px-10 py-4 text-center font-medium text-white hover:bg-opacity-90 lg:px-8 xl:px-10 w-full"
          >
            {LOGIN_FORM_CONST.LABELS.LOGIN}
          </button>
        </div>
      </form>
    );
  };

  return (
    <>
     {isLoading && <Loading />}
     <div className="flex items-center justify-center h-screen w-full px-5 sm:px-0">
      <div className="flex bg-white rounded-lg shadow-lg border overflow-hidden max-w-sm lg:max-w-4xl w-full">
        <div
          className="hidden md:block lg:w-1/2 bg-cover bg-blue-700"
          style={{
            backgroundImage: `url(https://www.tailwindtap.com//assets/components/form/userlogin/login_tailwindtap.jpg)`,
          }}
        ></div>
        <div className="w-full p-8 lg:w-1/2">
          {loginError && (
            <p className="text-red-600 font-semibold text-normal  text-center">
              {loginError}
            </p>
          )}
          {renderLoginForm()}
          <div className="grid grid-cols-2 gap-3 justify-center items-center mt-2 h-[44px]">
            <div className="flex w-full h-[44px] ">
              {renderSocialLogin(TEXT_CONSTANTS.GOOGLE)}
            </div>
            <div className="w-full h-[44px]">
              <FacebookLogin
                buttonStyle={{ padding: "6px", height: "100%" }}
                appId={process.env.REACT_APP_FACEBOOK_OAUTH_APP_ID} //tạo app để xài dịch vụ login trên facebook developer
                autoLoad={false}
                fields={FACEBOOK_LOGIN_CONST.FIELDS}
                render={(renderProps) => (
                  <>{renderSocialLogin(TEXT_CONSTANTS.FACEBOOK, renderProps)}</>
                )}
                callback={handleFacebookCallback}
              />
            </div>
          </div>
          <div className="mt-4 flex items-center w-full text-center">
            <Link
              href="#"
              className="text-xs text-gray-500 capitalize text-center w-full"
            >
              Don&apos;t have any account yet?
              <span className="text-blue-700"> Sign Up</span>
            </Link>
          </div>
        </div>
      </div>
    </div>
    </>
   
  );
};

export default LoginPage;
