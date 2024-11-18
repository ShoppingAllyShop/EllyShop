export const menuSidebarList = [
  {
    id: "f6f9e6f8-7ed6-4096-8881-8ed2a8f96f44",
    name: "Trang chủ",
    path: "/admin",
    icon: "home",
  },
  {
    id: "4b4042eb-feae-4fcf-9653-0681ae4a1094",
    name: "Quản lý sản phẩm",
    path: "",
    icon: "box-open",
    children: [
      {
        id: "87606271-bdce-449c-bc6c-b525fab04f89",
        name: "Danh mục",
        icon: "faThList",
        path: "/admin/category",
      },
      {
        id: "69ae0a2d-0485-4097-97d9-3dde2e914dee",
        name: "Sản phẩm",
        path: "/admin/product",
      },
    ],
  },
  {
    id: "bb0440a7-b91d-44d0-91eb-9bc2cd51762c",
    name: "Quản lý đơn hàng",
    path: "",
    icon: "cubes",
    children: [
      {
        id: "8443756e9-d220-4182-af62-82ca3a333d5d",
        name: "Đơn hàng",
        icon: "cub",
        path: "/admin/order",
      }
    ],
  },
  {
    id: "0a4aa569-9c35-476d-8ccf-65cdd7ac2c20",
    name: "Quản lý tài khoản",
    path: "",
    icon: "users",
    children: [
      {
        id: "0a4aa569-9c35-476d-8ccf-65cdd7ac2c21",
        name: "Tài khoản",
        icon: "user",
        path: "/admin/user",
      }
    ],
  }
];

