import React, { Fragment, useEffect } from "react";
import configData from "./Config";
import Axios from "axios";
import parse from "html-react-parser";

export default function HomePlace3(props) {
  const adsData = props.adHeader;
  const adImage = adsData ? adsData.pAsset.AssetLiveUrl : "";
  let parser = new DOMParser();
  let homeCategory1 = props.homeCategory.filter((category) => {
    return category.PlaceHolderIDForHome == 3;
  });
  let post0 = "";

  homeCategory1 = homeCategory1.length > 0 ? homeCategory1[0] : [];

  const axiosConfig = {
    headers: {
      sessionToken: configData.SESSION_TOKEN,
    },
  };

  const [postCrime, setPostCrime] = React.useState([]);
  const postApiUrl = configData.POST_API_URL.replace(
    "#CATEGORY_SLUG",
    homeCategory1.Slug
  ).replace("#OFFSET", "0");

  // Get postCrime categories
  useEffect(() => {
    if (homeCategory1 && homeCategory1.Slug) {
      // console.log("slug-" + homeCategory1.Slug);
      // console.log("api url-" + postApiUrl);
      Axios.get(postApiUrl, axiosConfig).then((response) => {
        // console.log("response-");
        // console.log(response.data.payload);
        setPostCrime(JSON.parse(response.data.payload));
      });
    }
  }, [homeCategory1.Slug]);
  return (
    <div className="col-lg-6">
      {/* <!-- News Tabs start --> */}
      <div className="main-content tab-view border-theme HomePlace3Space">
        <div className="row">
          <div className="col-lg-12 mycol">
            <div className="header-area">
              <h3 className="title">{homeCategory1.DefaultTitleToDisplay}</h3>
              <a href={`${configData.BASE_URL_CATEGORY}${homeCategory1.Slug}`}>
                सभी देखें
              </a>
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
                  <div className="col-md-6 mycol">
                    <div className="single-news landScape-normal box_design_Line HomePlace3">
                      <a
                        href={`${configData.BASE_URL_CATEGORY_DETAIL}${
                          postCrime && postCrime.length > 0
                            ? postCrime[0].Slug
                            : ""
                        }`}
                      >
                        <div className="content-wrapper">
                          <div className="img">
                          <div className="tag" style={postCrime && postCrime.length > 0 ? { backgroundColor: postCrime[0].CategoryColor } : { backgroundColor: '#9c27b0' }}>
                              {homeCategory1.DefaultTitleToDisplay}
                            </div>
                            <img
                              src={
                                postCrime && postCrime.length > 0
                                  ? postCrime[0].PostFiles[0].AssetLiveUrl
                                  : ""
                              }
                              alt={
                                postCrime && postCrime.length > 0
                                  ? postCrime[0].TitleData[0].Translation
                                  : ""
                              }
                              className="lazy"
                            />
                          </div>
                          <div className="inner-content">
                            <a
                              href={`${configData.BASE_URL_CATEGORY_DETAIL}${
                                postCrime && postCrime.length > 0
                                  ? postCrime[0].Slug
                                  : ""
                              }`}
                            >
                              <h4 className="title font-10">
                                {postCrime && postCrime.length > 0
                                  ? postCrime[0].TitleData[0].Translation
                                  : ""}
                              </h4>
                              <p className="text">
                                {postCrime && postCrime.length > 0
                                  ? parse(
                                      postCrime[0].DescriptionData[0].Translation.substring(
                                        0,
                                        175
                                      ) + "..."
                                    )
                                  : ""}
                              </p>
                            </a>
                          </div>
                        </div>
                      </a>
                    </div>
                  </div>
                  <div className="col-md-6 mycol">
                    <div className="row">
                      {postCrime && postCrime.length > 0
                        ? postCrime.map((p, i) => {
                            if (i > 0 && i < 6) {
                              return (
                                <div className="col-md-12 r-p">
                                  <div className="single-box landScape-small-with-meta box_design_Line">
                                    <div className="content border_1">
                                      <a
                                        href={`${configData.BASE_URL_CATEGORY_DETAIL}${p.Slug}`}
                                      >
                                        <h4 className="title_long angleRight">
                                          {p.TitleData[0].Translation}
                                        </h4>
                                      </a>
                                    </div>
                                  </div>
                                </div>
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
  );
}
