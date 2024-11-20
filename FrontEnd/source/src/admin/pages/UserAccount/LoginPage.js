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
import { PUPLIC_ENDPOINT } from "../../../constants/endpoint";
import Loading from "../../../components/Loading";

const LoginPage = ({ handleTest }) => {
  const navigate = useNavigate();
  const axiosBase = useAxiosBase();
  const dispatch = useDispatch();
  const [loginError, setLoginError] = useState("");
  const [isLoading, setIsLoading] = useState(false)

  const schema = yup
    .object({
      email: yup
        .string()
        .required("Vui lòng điền email")
        .email("Email không hợp lệ"),
      password: yup.string().required("Vui lòng điền password"),
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
    console.log("response", response);
    const params = {
      token: response.access_token,
      pageType: checkIsAdminPage() ? "AdminPage" : "ClientPage",
      provider: AUTH_PROVIDERS.GOOGLE,
    };

    console.log("params", params);
    const endpoint = PUPLIC_ENDPOINT.SOCIAL_LOGIN;
    const [loginResponse] = await Promise.all([
      axiosBase.post(endpoint, params),
    ]);

    hanldeLoginResponse(loginResponse);
  };

  const googleErrorMessage = (error) => {
    console.log(error);
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
    const endpoint = PUPLIC_ENDPOINT.SOCIAL_LOGIN;
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
      const endpoint = PUPLIC_ENDPOINT.LOGIN;
      const [loginResponse] = await Promise.all([
        axiosBase.post(endpoint, params),
        setIsLoading(true)
      ]);
      hanldeLoginResponse(loginResponse);
    } catch (error) {
       hanldeLoginResponse(error);  
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
    navigate("/admin/dashboard");
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
        onClick={
          type !== TEXT_CONSTANTS.FACEBOOK ? loginFunc : provider.onClick
        }
        className="flex h-full w-full bg-white border border-gray-300 px-3 items-center
      text-[12px] font-medium text-gray-800 hover:bg-gray-100"
      >
        <img src={icon} alt="" className="size-7" />
        <span className="ml-1">Login with {capitalizeFirstLetter(type)}</span>
      </button>
    );
  };

  const renderLoginForm = () => {
    return (
      <form onSubmit={handleSubmit(onSubmitLoginForm)}>
        <div className="mt-4">
          <InputField
            label="Email"
            name="email"
            register={register}
            errors={errors}
            className="text-gray-700 border border-gray-300 rounded py-2 px-4 block w-full focus:outline-2 focus:outline-blue-700"
          />
        </div>
        <div className="mt-4 flex flex-col justify-between">
          <InputField
            label="Password"
            name="password"
            type="password"
            register={register}
            errors={errors}
            className="text-gray-700 border border-gray-300 rounded py-2 px-4 block w-full focus:outline-2 focus:outline-blue-700"
          />
          <Link
            href="#"
            className="text-xs text-gray-500 hover:text-gray-900 text-end w-full mt-2"
          >
            Forget Password phai khong?
          </Link>
        </div>
        <div className="mt-8">
          <button
            type="submit"
            className="bg-blue-700 text-white font-bold py-2 px-4 w-full rounded hover:bg-blue-600"
          >
            Đăng nhập douma
          </button>
        </div>
      </form>
    );
  };

  const Test = () => {
    const [stateNe, setStateNe] = useState({});
    const src = "https://images.unsplash.com/photo-1444065381814-865dc9da92c0";
    const handleMouseMove = (e) => {
      const { left, top, width, height } = e.target.getBoundingClientRect();
      const x = ((e.pageX - left) / width) * 100;
      const y = ((e.pageY - top) / height) * 100;
      setStateNe({
        backgroundImage: `url(${src})`,
        backgroundPosition: `${x}% ${y}%`,
      });
    };
    return (
      <figure
        onMouseMove={handleMouseMove}
        style={stateNe}
        className="group bg-no-repeat block"
      >
        <img src={src} alt="" className="group-hover:opacity-0 opacity-0" />
      </figure>
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
