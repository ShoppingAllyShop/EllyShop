
# Sử dụng image node để build ứng dụng
FROM node:18 AS build

# Đặt thư mục làm việc trong container
WORKDIR /app

# Sao chép package.json và package-lock.json vào container
COPY /FrontEnd/source/package.json /FrontEnd/source/package-lock.json ./
# Cài đặt các dependencies
RUN npm install --legacy-peer-deps

# Sao chép toàn bộ mã nguồn vào container D:\Lap Trinh\Projects\Web\EllyShop\FrontEnd\source\.nginx\nginx.conf
COPY /FrontEnd/source ./

#Set env variables
ARG REACT_APP_BASE_API_URL
ARG REACT_APP_FACEBOOK_OAUTH_APP_ID
ARG REACT_APP_GOOGLE_OAUTH_CLIENT_ID

ENV REACT_APP_BASE_API_URL $REACT_APP_BASE_API_URL
ENV REACT_APP_FACEBOOK_OAUTH_APP_ID $REACT_APP_FACEBOOK_OAUTH_APP_ID
ENV REACT_APP_GOOGLE_OAUTH_CLIENT_ID $REACT_APP_GOOGLE_OAUTH_CLIENT_ID

# Biên dịch ứng dụng React
RUN npm run build

# Chạy server nhẹ để phục vụ React
FROM node:18-alpine AS production
ENV NODE_ENV=production
WORKDIR /app
COPY --from=build /app/build /app/build

RUN npm install -g serve

# Chỉ định cổng mà Nginx sẽ chạy
EXPOSE 3000

# Lệnh để chạy Nginx
CMD ["serve", "-s", "build", "-l", "3000"]

# Sử dụng Nginx để phục vụ ứng dụng
FROM nginx:alpine AS development
ENV NODE_ENV=development
# Sao chép các file build từ image trước vào thư mục Nginx
COPY --from=build /app/build /usr/share/nginx/html

# Copy file cấu hình Nginx tùy chỉnh nếu cần
COPY FrontEnd/.nginx/nginx.conf /etc/nginx/conf.d/default.conf

# Chỉ định cổng mà Nginx sẽ chạy
EXPOSE 3000

# Lệnh để chạy Nginx
CMD ["nginx", "-g", "daemon off;"]