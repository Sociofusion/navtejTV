import React, { Fragment, useEffect } from "react";
import Footer from "./Footer";
import HeroAreaCategory from "./HeroAreaCategory";
import MainMenu from "./MainMenu";
import MobileMenu from "./MobileMenu";
import RelatedNews from "./RelatedNews";
import AdArea from "./AdArea";
import TopHeader from "./TopHeader";
import TopHeaderScroll from "./TopHeaderScroll";
import configData from "./Config";
import Axios from "axios";
import { useParams } from "react-router-dom";
import parse from "html-react-parser";
import Newsletter from "./Newsletter";
import {
  WhatsappShareButton,
  FacebookShareButton,
  TwitterShareButton,
} from "react-share";
import { FacebookEmbed } from "react-social-media-embed";
import { InstagramEmbed } from "react-social-media-embed";
import { TwitterEmbed } from "react-social-media-embed";
import OwlCarousel from "react-owl-carousel";

export default function CategoryDetailPage(props) {
  const menus = props.menus;
  const site_lang = props.site_lang;
  const { postSlug } = useParams();
  const postDetailApiUrl = configData.POST_DETAIL_API_URL + postSlug;
  const [post, setPost] = React.useState([]);
  const axiosConfig = {
    headers: {
      sessionToken: configData.SESSION_TOKEN,
    },
  };

  const adHeader = props.adHeader;

  const adsDataFooter = props.adFooter;
  const adImageFooter = adsDataFooter ? adsDataFooter.pAsset.AssetLiveUrl : "";
  const adFooterLink = adsDataFooter ? adsDataFooter.AdLink : "";

  const adsDataRight = props.adRight;
  const adImageRight = adsDataRight ? adsDataRight.pAsset.AssetLiveUrl : "";
  const adRightLink = adsDataRight ? adsDataRight.AdLink : "";

  const adsDataRight2 = props.adRight2;
  const adImageRight2 = adsDataRight2 ? adsDataRight2.pAsset.AssetLiveUrl : "";
  const adRightLink2 = adsDataRight2 ? adsDataRight2.AdLink : "";

  const settingData = props.settingData;

  // Get post categories
  useEffect(() => {
    Axios.get(postDetailApiUrl, axiosConfig).then((response) => {
      // console.log("data from server", JSON.parse(response.data.payload));
      // console.log("data from server used api", postDetailApiUrl);
      const data = JSON.parse(response.data.payload);
      setPost(data);
    });
  }, []);

  if (!post) return null;
  return (
    <div>
      {/* <!-- preloader area end --> */}
      {/* <!-- Top Header Area Start --> */}
      <TopHeader todayDate={props.todayDate} />
      {/* <!-- Top Header Area End --> */}
      {/* <!--Main-Menu Area Start--> */}
      <MainMenu menus={menus} />
      {/* <!--Main-Menu Area Start--> */}
      {/* <!-- Mobile Menu Area Start --> */}
      <MobileMenu menus={menus} />
      {/* <!-- Mobile Menu Area End --> */}
      {/* <!-- Header Part End--> */}
      {/* <!--Content of each page--> */}
      {/* <!-- Top Header Area Start --> */}

      <TopHeaderScroll marqueeNews={props.marqueeNews} site_lang={site_lang} />
      <AdArea adHeader={props.adHeader} />
      {/* <!-- Top Header Area End --> */}
      {/* <!-- Hero Area Start --> */}
      <section className="hero-area news-details-page home-front-area">
        <div className="container">
          <div className="row">
            <div className="col-lg-8">
              <div className="details-content-area">
                <div className="row">
                  <div className="col-lg-12 details-post">
                    <div className="single-news">
                      <h4 className="title">
                        {console.log("Post Details", post)}
                        {post && post.TitleData
                          ? post.TitleData[0].Translation
                          : ""}
                      </h4>

                      <div className="post-footer">
                        <div
                          className="a2a_kit a2a_kit_size_32 a2a_default_style"
                          style={{ lineHeight: "32px" }}
                        >
                          <span className="date-social">
                            {" "}
                            {post.CreatedOnStr}
                          </span>
                          <ul className="social-share">
                            <FacebookShareButton
                              url={`http://navtej.sixwares.com/details/${postSlug}`}
                            >
                              <li>
                                <a
                                  className="a2a_button_facebook"
                                  href="javascript:void(0)"
                                  rel="nofollow noopener"
                                >
                                  <i className="fab fa-facebook-f"></i>
                                </a>
                              </li>
                            </FacebookShareButton>

                            <TwitterShareButton
                              url={`http://navtej.sixwares.com/details/${postSlug}`}
                            >
                              <li>
                                <a
                                  className="a2a_button_twitter"
                                  href="javascript:void(0)"
                                  rel="nofollow noopener"
                                >
                                  <i className="fab fa-twitter"></i>
                                </a>
                              </li>
                            </TwitterShareButton>
                            {/* <InstapaperShareButton url={`http://navtej.sixwares.com/details/${postSlug}`}>
                              <li>
                                <a className="a2a_button_instagram"
                                  href="javascript:void(0)" rel="nofollow noopener">
                                  <i className="fab fa-instagram"></i>
                                </a>
                              </li>
                            </InstapaperShareButton> */}
                            <WhatsappShareButton
                              url={`http://navtej.sixwares.com/details/${postSlug}`}
                            >
                              <li>
                                <a
                                  className="a2a_button_twitter-"
                                  href="javascript:void(0)"
                                  rel="nofollow noopener"
                                >
                                  <i
                                    className="fab fa-whatsapp"
                                    aria-hidden="true"
                                  ></i>
                                </a>
                              </li>
                            </WhatsappShareButton>
                          </ul>
                          <div style={{ clear: "both" }}></div>
                        </div>

                        <script
                          async=""
                          src="https://static.addtoany.com/menu/page.js"
                        ></script>
                      </div>
                      <div className="img">
                        <div className="tag" style={post && post.TitleData ? { backgroundColor: post.CategoryColor } : { backgroundColor: '#9c27b0' }}>
                          {post.CategoryName}</div>
                        <img
                          src={
                            post && post.PostFiles && post.PostFiles.length > 0
                              ? post.PostFiles[0].AssetLiveUrl
                              : ""
                          }
                          alt={
                            post && post.TitleData
                              ? post.TitleData[0].Translation
                              : ""
                          }
                        />
                      </div>

                      <div className="content">
                        {post && post.DescriptionData
                          ? parse(post.DescriptionData[0].Translation)
                          : ""}
                      </div>

                      <div className="content">
                        <OwlCarousel
                          className="owl-theme"
                          items="1"
                          autoplay
                          loop
                        >

                          {post && post.PostFiles && post.PostFiles.length > 1 ? post.PostFiles.map((p, i) => {
                            return (
                              <div className="item intro-carousel">
                                <div className="content-wrapper">
                                  <img
                                    src={
                                      post && post.PostFiles && post.PostFiles.length > 0
                                        ? post.PostFiles[i].AssetLiveUrl
                                        : "/assets/images/no-image.png"
                                    }
                                    alt=""
                                  />
                                </div>
                              </div>
                            );
                          }) : ""}
                        </OwlCarousel>
                      </div>

                      <div className="content">
                        {post.ISFacebookEmbed ? (
                          <div
                            style={{
                              display: "flex",
                              justifyContent: "center",
                            }}
                          >
                            <FacebookEmbed
                              url={post ? post.EmbedSocial : ""}
                              width={550}
                            />
                          </div>
                        ) : (
                          ""
                        )}

                        {post.ISInstagramEmbed ? (
                          <div
                            style={{
                              display: "flex",
                              justifyContent: "center",
                            }}
                          >
                            <InstagramEmbed
                              url={post ? post.EmbedSocial : ""}
                              width={550}
                              captioned
                            />
                          </div>
                        ) : (
                          ""
                        )}

                        {post.ISTwitterEmbed ? (
                          <div
                            style={{
                              display: "flex",
                              justifyContent: "center",
                            }}
                          >
                            <TwitterEmbed
                              url={post ? post.EmbedSocial : ""}
                              width={550}
                            />
                          </div>
                        ) : (
                          ""
                        )}
                      </div>
                    </div>
                  </div>
                </div>
              </div>
            </div>

            <div className="col-lg-4">
              <div className="row">
                <div className="col-lg-12">
                  <div className="side-video">
                    <iframe
                      frameBorder="0"
                      scrolling="no"
                      marginHeight="0"
                      marginWidth="0"
                      width="100%"
                      height="200"
                      type="text/html"
                      src={settingData.YoutubeVideoURL}
                    ></iframe>
                  </div>
                </div>
              </div>

              {/* <!-- News Tabs start --> */}

              <div className="row">
                <div className="col-lg-12">
                  <div className="ad-area">
                    <a href={adRightLink} target="_blank">
                      <img src={adImageRight} />
                    </a>
                  </div>
                </div>
              </div>

              {/* <!-- News Tabs start --> */}
              <Newsletter adRight3={props.adRight3} />

              <div className="row">
                <div className="col-lg-12">
                  <div className="ad-area">
                    <a href={adRightLink2} target="_blank">
                      <img src={adImageRight2} />
                    </a>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </section>
      {/* <!-- Hero Area End --> */}
      {/* <!--  देश विदेश Area Start --> */}
      <RelatedNews postId={post.ID} />
      {/* <!--  देश विदेश Area End --> */}
      <section className="home-front-area">
        <div className="container">
          <div className="row">
            <div className="col-lg-12">
              {/* <!-- News Tabs start --> */}
              <div className="main-content tab-view">
                <div className="row">
                  <div className="col-lg-12 mycol padding_15">
                    <a href={adFooterLink} target="_blank"> <img src={adImageFooter} /></a>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </section>
      {/* <!-- Footer Area Start --> */}
      <Footer footerMenu={props.footerMenu} settingData={settingData} />
      {/* <!-- Footer Area End --> */}
      {/* <!-- Back to Top Start --> */}
      <div className="bottomtotop">
        <i className="fas fa-chevron-right"></i>
      </div>
      {/* <!-- Back to Top End --> */}
    </div>
  );
}
