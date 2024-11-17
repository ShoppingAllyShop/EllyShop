//call ContentManagement Api
const contentMainPageResponse = {
    layoutData: {
        header:{
            navigations:[{},{}],
        },
        footer:{
            policies:[{},{}],
            socialMedias:[{},{}],
            GeneralInfomations:[{},{}],
        }
    },
    mainPageData: {
        prizes:[{},{}],
        showroom:[{},{}],
        news:[{},{}],
        newsMedia:[{},{}],
        slider:[{}],
    }
}

//call Câtalog Api
const catalogDataResponse = {
    categoryResponse: [{},{}],
    collectionResponse :[{},{}],
    productResponse : {
        newProducts:[{},{},{},{}],
        bestSellers:[{},{}],
        favourites:[{},{}],
    }

}

// call ProductDetail API
//contentmanagementApi
const productDetailContent = {
    showroom:[{},{}],
}

//câtalogApi
const productDetailData = {
    guarantee:[{},{}],
    guide:[{},{}],
    productImage:[{},{}],
    productDetail:[{},{}],
    product:{},
    color:[{}],
    size:[{}],
    favoriteProducts:[{},{}],
    relativeProducts:[{},{}],
    rating:[{}]
}
