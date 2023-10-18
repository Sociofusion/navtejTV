import React, { Fragment, useEffect } from "react";
import configData from "./Config";
import Axios from "axios";
import parse from "html-react-parser";

export default function HomePlace4(props) {
  const adsData = props.adHeader;
  const adImage = adsData ? adsData.pAsset.AssetLiveUrl : "";
  let parser = new DOMParser();
  let homeCategory1 = props.homeCategory.filter((category) => {
    return category.PlaceHolderIDForHome == 4;
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
                      <h3 className="title">{homeCategory1.DefaultTitleToDisplay}</h3>
                      <a href={`${configData.BASE_URL_CATEGORY}${homeCategory1.Slug}`}>सभी देखें</a>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </section>
      <section className="">
        <div className="container">
          <div className="row">
          {post && post.length > 0 ? post.map((p, i) => {
                                if(i<3){
                                  return(
            <div className="col-lg-4">
              <a href={`${configData.BASE_URL_CATEGORY_DETAIL}${p.Slug}`} className="single-news animation">
                <div className="content-wrapper heightBig HomePlace4_1">
                <div className="tag" style={post && post.length > 0 ? { backgroundColor: p.CategoryColor } : { backgroundColor: '#9c27b0' }}>{homeCategory1.DefaultTitleToDisplay}</div>
                  <img
                    src={p.PostFiles[0].AssetLiveUrl}
                    alt={p.TitleData[0].Translation}
                    className="lazy"
                  />
                  <div className="inner-content">
                    <span className="white-section">
                      <h4 className="title">
                      {p.TitleData[0].Translation}
                      </h4>
                    </span>
                  </div>
                </div>
              </a>
            </div>
            )
        }
      }) : ''}

          </div>
        </div>
      </section>
      <section className="home-front-area">
        <div className="container">
          <div className="row">
            <div className="col-lg-8">
              <a href={`${configData.BASE_URL_CATEGORY_DETAIL}${post && post.length > 0 ? post[3].Slug:''}`} className="single-news animation">
                <div className="content-wrapper heightBig HomePlace4_2">
                <div className="tag" style={post && post.length > 0 ? { backgroundColor: post[3].CategoryColor } : { backgroundColor: '#9c27b0' }}>
                  {homeCategory1.DefaultTitleToDisplay}</div>
                  <img
                    src={post && post.length > 0 ? post[3].PostFiles[0].AssetLiveUrl : ''}
                    alt={post && post.length > 0 ? post[3].TitleData[0].Translation : ''}
                    className="lazy"
                  />
                  <div className="inner-content">
                    <span className="white-section">
                      <h4 className="title">
                        {post && post.length > 0 ? post[3].TitleData[0].Translation : ''}
                      </h4>
                      {post && post.length > 0 ? parse(post[3].DescriptionData[0].Translation.substring(0,200)):''}
                    </span>
                  </div>
                </div>
              </a>
            </div>

            <div className="col-lg-4">
              <a href={`${configData.BASE_URL_CATEGORY_DETAIL}${post && post.length > 0 ? post[4].Slug:''}`} className="single-news animation">
                <div className="content-wrapper heightBig HomePlace4_3">
                <div className="tag" style={post && post.length > 0 ? { backgroundColor: post[4].CategoryColor } : { backgroundColor: '#9c27b0' }}>
                    {homeCategory1.DefaultTitleToDisplay}</div>
                  <img
                    src={post && post.length > 0 ? post[4].PostFiles[0].AssetLiveUrl:''}
                    alt={post && post.length > 0 ? post[4].TitleData[0].Translation : ''}
                    className="lazy"
                  />
                  <div className="inner-content">
                    <span className="white-section">
                      <h4 className="title">
                      {post && post.length > 0 ? post[4].TitleData[0].Translation : ''}
                      </h4>
                    </span>
                  </div>
                </div>
              </a>
            </div>
          </div>
        </div>
      </section>
    </Fragment>
  );
}
