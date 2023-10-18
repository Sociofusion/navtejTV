import React, { Fragment, useEffect, useState } from "react";
import Footer from "./Footer";
import HeroAreaCategory from "./HeroAreaCategory";
import MainMenu from "./MainMenu";
import MobileMenu from "./MobileMenu";
import RelatedNews from "./RelatedNews";
import TopHeader from "./TopHeader";
import TopHeaderScroll from "./TopHeaderScroll";
import configData from "./Config";
import Axios from "axios";
import { useParams, useLocation } from "react-router-dom";
import parse from "html-react-parser";
import Newsletter from "./Newsletter";
import AdArea from './AdArea'
import AdAreaSkeleton from './AdAreaSkeleton'

export default function Features(props) {
    const [customPages, setCustomPages] = useState([]);
    const axiosConfig = {
        headers: {
            sessionToken: configData.SESSION_TOKEN,
        },
    };
    const site_lang = props.site_lang;
    const menus = props.menus;
    const location = useLocation();
    const slug = location.pathname.replace('/', '');
    const customPageURL = configData.GET_CUSTOM_PAGE + slug;

    const adHeader = props.adHeader;

    const adsDataFooter = props.adFooter;
    const adImageFooter = adsDataFooter ? adsDataFooter.pAsset.AssetLiveUrl : '';
    const adFooterLink = adsDataFooter ? adsDataFooter.AdLink : '';

    const adsDataRight = props.adRight;
    const adImageRight = adsDataRight ? adsDataRight.pAsset.AssetLiveUrl : '';
    const adRightLink = adsDataRight ? adsDataRight.AdLink : '';

    const settingData = props.settingData;

    // Getting Custom Page
    useEffect(() => {
        Axios.get(customPageURL, axiosConfig).then((response) => {
            setCustomPages(JSON.parse(response.data.payload));
        });

    }, []);


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
            {/* <!-- Top Header Area End --> */}

            {
                adHeader && adHeader.pAsset != '' ?
                    <AdArea adHeader={adHeader} />
                    :
                    <AdAreaSkeleton />
            }

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
                                                {customPages && customPages.TitleData ? parse(customPages.TitleData[0].Translation) : ''}
                                            </h4>

                                            {customPages && customPages.DescriptionData ? parse(customPages.DescriptionData[0].Translation) : ''}
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div className="col-lg-4">
                            <div className="row">
                                <div className="col-lg-12">
                                    <div className="side-video">
                                        <iframe frameBorder="0" scrolling="no" marginHeight="0" marginWidth="0" width="100%" height="220"
                                            type="text/html" src={settingData.YoutubeVideoURL}></iframe>
                                    </div>
                                </div>
                            </div>
                            {/* <!-- News Tabs start --> */}
                            {/* <!-- News Tabs start --> */}
                            <Newsletter adRight3={props.adRight3} />
                            <div className="row">
                                <div className="col-lg-12">
                                    <div className="ad-area">
                                        <a href={adRightLink} target="_blank"> <img src={adImageRight} /> </a>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </section>
            {/* <!-- Hero Area End --> */}
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
            <Footer footerMenu={props.footerMenu} settingData={settingData}/>
            {/* <!-- Footer Area End --> */}
            {/* <!-- Back to Top Start --> */}
            <div className="bottomtotop">
                <i className="fas fa-chevron-right"></i>
            </div>
            {/* <!-- Back to Top End --> */}
        </div>
    )
}