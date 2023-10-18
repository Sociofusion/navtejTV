import React, { Fragment, useEffect } from "react";
import configData from "./Config";
import Axios from "axios";
import parse from "html-react-parser";

export default function HomePlace6(props) {
  const adsData = props.adHeader;
  const adImage = adsData ? adsData.pAsset.AssetLiveUrl : "";
  let parser = new DOMParser();
  let homeCategory1 = props.homeCategory.filter((category) => {
    return category.PlaceHolderIDForHome == 6;
  });
  let post0 = "";

  homeCategory1 = homeCategory1.length > 0 ? homeCategory1[0] : [];

  const axiosConfig = {
    headers: {
      sessionToken: configData.SESSION_TOKEN,
    },
  };

  const [post, setPost] = React.useState([]);

  // Get post categories
  useEffect(() => {
    if(homeCategory1 && homeCategory1.Slug){
    const postApiUrl = configData.POST_API_URL.replace(
      "#CATEGORY_SLUG",
      homeCategory1.Slug
    ).replace('#OFFSET','0');
    Axios.get(postApiUrl, axiosConfig).then((response) => {
      setPost(JSON.parse(response.data.payload));
    });
  }
  }, [homeCategory1.Slug]);

  return (
    <Fragment>
      <section className="home-front-area">
        <div className="container">
          <div className="row">
            <div className="col-lg-12">
              {/* <!-- News Tabs start --> */}
              <div className="main-content tab-view border-theme">
                <div className="row">
                  <div className="col-lg-12 mycol">
                    <div className="header-area">
                      <h3 className="title">
                        {homeCategory1.DefaultTitleToDisplay}
                      </h3>
                      <a href={`${configData.BASE_URL_CATEGORY}${homeCategory1.Slug}`}>सभी देखें</a>
                    </div>
                  </div>
                </div>
                <div className="row">
                  <div className="col-lg-12">
                    <div className="tab-content" id="pills-tabContent">
                      <div
                        className="tab-pane fade show active"
                        id="pills-Rajasthan"
                        role="tabpanel"
                        aria-labelledby="pills-Rajasthan-tab"
                      >
                        <div className="row">
                          <div className="col-md-8 mycol">
                            <div className="single-news landScape-normal box_design_block HomePlace6_1">
                              <a href={`${configData.BASE_URL_CATEGORY_DETAIL}${post && post.length > 0
                                          ? post[0].Slug:''}`}>
                                <div className="content-wrapper">
                                  <div className="img">
                                  <div className="tag" style={post && post.length > 0 ? { backgroundColor: post[0].CategoryColor } : { backgroundColor: '#9c27b0' }}>
                                      {homeCategory1.DefaultTitleToDisplay}
                                    </div>
                                    <img
                                      src={
                                        post && post.length > 0
                                          ? post[0].PostFiles[0].AssetLiveUrl
                                          : ""
                                      }
                                      alt=""
                                      className="lazy"
                                    />
                                  </div>
                                  <div className="inner-content">
                                    <a href={`${configData.BASE_URL_CATEGORY_DETAIL}${post && post.length > 0
                                          ? post[0].Slug:''}`}>
                                      <h4 className="title">
                                        {post && post.length > 0
                                          ? post[0].TitleData[0].Translation
                                          : ""}
                                      </h4>
                                      <p className="text">
                                        {post && post.length > 0
                                          ? parse(
                                              post[0].DescriptionData[0].Translation.substring(
                                                0,
                                                180
                                              )
                                            )
                                          : ""}
                                      </p>
                                    </a>
                                  </div>
                                </div>
                              </a>
                            </div>
                          </div>
                          <div className="col-md-4 mycol">
                            <div className="row r-p">
                              {post && post.length > 0
                                ? post.map((p, i) => {
                                    if (i > 0 && i < 6) {
                                      return (
                                        <a href={`${configData.BASE_URL_CATEGORY_DETAIL}${
                                          p.Slug}`}>
                                          <div className="single-box landScape-small-with-meta box_design_line_img height85 HomePlace6_2">
                                            <div className="img height85 col-md-4 r-p">
                                              <img
                                                src={
                                                  p.PostFiles[0].AssetLiveUrl
                                                }
                                                alt=""
                                                className="lazy"
                                              />
                                            </div>
                                            <div className="content col-md-8">
                                              <h4 className="title">
                                                {p.TitleData[0].Translation}
                                              </h4>
                                            </div>
                                          </div>
                                        </a>
                                      );
                                    }
                                  })
                                : ""}
                            </div>
                          </div>
                        </div>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </section>
    </Fragment>
  );
}
