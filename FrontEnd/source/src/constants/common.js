//local storage key
export const USER_LOCAL_STORAGE_KEY = "elly_user";

//Button name
export const BUTTON_NAME = {
  INFO: "Thông tin",
  SETTING: "Cài đặt",
  LOGOUT: "Đăng xuất",
  AGREE: "Đồng ý",
  CLOSE: "Đóng",
  EDIT: "Chỉnh sửa",
  ADD: "Thêm"
};

//Button type
export const BUTTON_TYPE = {
  INFO: "Infomation",
  SETTING: "Setting",
  LOGOUT: "Logout",
  AGREE: "Agree",
  CLOSE: "Close",
  EDIT: "Edit",
  ADD: "Add"
};

//Guid Id
export const GUID_ID = {
    ADMIN_ROLE: '5A387B6E-1B29-47E1-84F6-D25D4287B11C',
    CUSTOMER_ROLE:'87A989E6-C641-4222-AF8B-9CFC3C9B1183'
}

//text
export const TEXT_CONSTANTS = {
  FACEBOOK: 'Facebook',
  GOOGLE: 'Google',

}

//Facebook login lib
export const FACEBOOK_LOGIN_CONST = {
  FIELDS: "name,email,picture"
}

export const AUTH_PROVIDERS = {
  FACEBOOK: 0,
  GOOGLE: 1
}

export const STORAGE_TYPE = {
  SESSION: 'sessionStorage',
  LOCAL: 'localStorage'
}

export const RESPONSE_API_STATUS = {
  SUCCESS:'Success',
  ERROR: 'Error',
  ERROR_NETWORK: 'ERR_NETWORK'
}

//internal role in company except customer
export const INTERNAL_ROLES_ARRAY = [
  'Admin', 'Store Manager'
]