import React, { Fragment, useEffect, useState } from "react";
import configData from "../Config";
import Axios from "axios";
import parse from "html-react-parser";

export default function StatePlace1(props) {
  const adsData = props.adHeader;
  const adImage = adsData ? adsData.pAsset.AssetLiveUrl : "";

  let post0 = "";
  const postApiUrl = configData.POST_API_URL.replace(
    "#CATEGORY_SLUG",
    props.stateslug
  ).replace('#OFFSET', '0');

  const axiosConfig = {
    headers: {
      sessionToken: configData.SESSION_TOKEN,
    },
  };

  const [post, setPost] = useState(null);

  // Get post categories
  useEffect(() => {
    if (props.stateslug) {
      Axios.get(postApiUrl, axiosConfig).then((response) => {
        setPost(JSON.parse(response.data.payload));
      })
    }
  }, [props.stateslug]);

  if (!post) return null;

  return (
    <Fragment>
      <section className="home-front-area">
        <div className="container">
          <div className="row">
            <div className="col-lg-8">
              {/* <!-- News Tabs start --> */}
              <div className="main-content tab-view border-theme">
                <div className="row">
                  <div className="col-lg-12 mycol">
                    <div className="header-area">
                      <h3 className="title">
                        {props.stateslug}
                      </h3>
                      <a href={`${configData.BASE_URL_CATEGORY}${props.stateslug}`}>सभी देखें</a>
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
                            <div className="single-news landScape-normal box_design_block HomePlace1">
                              <a href={`${configData.BASE_URL_CATEGORY_DETAIL}${post && post.length > 0 ? post[0].Slug : ''}`}>
                                <div className="content-wrapper">
                                  <div className="img">
                                    <div className="tag" style={post && post.length > 0 ? { backgroundColor: post[0].CategoryColor } : { backgroundColor: '#9c27b0' }}>
                                      {props.stateslug}
                                    </div>
                                    
                                    <img
                                      src={post && post.length > 0 && post[0].PostFiles.length > 0 ? post[0].PostFiles[0].AssetLiveUrl : '/assets/images/no-image.png'}
                                      alt={post[0].TitleData[0].Translation}
                                      className="lazy"
                                    />


                                  </div>
                                  <div className="inner-content">
                                    <a href={`${configData.BASE_URL_CATEGORY_DETAIL}${post && post.length > 0 ? post[0].Slug : ''}`}>
                                      <h4 className="title">
                                        {post && post.length > 0
                                          ? post[0].TitleData[0].Translation.substring(0, 40) + ' ...'
                                          : ""}
                                      </h4>
                                      <p className="text">
                                        {post && post.length > 0 ? parse(
                                          post[0].DescriptionData[0].Translation.substring(0,180) + ' ...'
                                        ) : ''}
                                      </p>
                                    </a>
                                  </div>
                                </div>
                              </a>
                            </div>
                          </div>
                          <div className="col-md-6 mycol">
                            <div className="row">
                              {post && post.length > 0 ? post.map((p, i) => {
                                if (i > 0 && i < 5) {
                                  return (
                                    <div className="col-md-6 r-p">
                                      <a href={`${configData.BASE_URL_CATEGORY_DETAIL}${p.Slug}`}>
                                        <div className="single-box landScape-small-with-meta box_design_block">
                                          <div className="img">
                                            <img
                                              src={p && p.PostFiles && p.PostFiles.length > 0 ? p.PostFiles[0].AssetLiveUrl : "/assets/images/no-image.png"}
                                              alt={p.TitleData[0].Translation}
                                              className="lazy"
                                            />
                                          </div>
                                          <div className="content">
                                            <a href={`${configData.BASE_URL_CATEGORY_DETAIL}${p.Slug}`}>
                                              <h4 className="title">
                                                {p.TitleData[0].Translation}
                                              </h4>
                                            </a>
                                          </div>
                                        </div>
                                      </a>
                                    </div>
                                  )
                                }
                              }) : ''}
                            </div>
                          </div>
                        </div>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
            </div>

            <div className="col-lg-4">
              {/* <!-- सोशल मीडिया start --> */}
              <div className="main-content tab-view border-theme">
                <div className="row">
                  <div className="col-lg-12 mycol">
                    <div className="header-area">
                      <h3 className="title">सोशल मीडिया</h3>
                    </div>
                  </div>
                </div>
                <div className="row">
                  <div className="col-lg-12 mycol">
                    <div className="blank"></div>
                  </div>
                </div>
              </div>
              {/* <!-- सोशल मीडिया emd --> */}
            </div>
          </div>
        </div>
      </section>
    </Fragment>
  );
}
