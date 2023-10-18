import React, { useEffect, useState } from "react";
import HeroAreaCategory from "../HeroAreaCategory";
import SearchList from "../SearchList";
import MainMenu from "../MainMenu";
import MobileMenu from "../MobileMenu";
import TopHeader from "../TopHeader";
import TopHeaderScroll from "../TopHeaderScroll";
import { useSearchParams } from "react-router-dom";
import Footer from "../Footer";
import HomePlace2 from "../HomePlace2";
import HomePlace4 from "../HomePlace4";
import HomePlace5 from "../HomePlace5";
import HomePlace6 from "../HomePlace6";
import Axios from "axios";
import StatePlace1 from "./StatePlace1";
import configData from "../Config";
import StateHeroArea from "./StateHeroArea";
import { useParams } from "react-router-dom"
import AdArea from "../AdArea";
import AdAreaSkeleton from "../AdAreaSkeleton";
import HeroAreaSkeleton from "../HeroAreaSkeleton";
import HomePlace1Skeleton from "../HomePlace1Skeleton";
import HomePlace7 from "../HomePlace7";
export default function State(props) {

    const { stateslug } = useParams()

    const State_SLIDER_API_URL = configData.baseApiUrl + "getPosts?languageId=" + configData.languageId + "&offset=0&itemcount=10&categoryId=0&isFeature=0&isSlider=1&isSliderLeft=0&isSliderRight=0&isTrending=0&categorySlug=" + stateslug
    const State_RIGHT_SLIDER_API_URL = configData.baseApiUrl + "getPosts?languageId=" + configData.languageId + "&offset=0&itemcount=10&categoryId=0&isFeature=0&isSlider=0&isSliderLeft=0&isSliderRight=1&isTrending=0&categorySlug=" + stateslug

    const menus = props.menus;
    const site_lang = props.site_lang;
    const marqueeNews = props.marqueeNews;
    const [homeCategory, setHomeCategory] = React.useState([]);
    const [StateSliderData, setStateSliderData] = useState([])
    const [StateRightSliderData, setStateRightSliderData] = useState([])
    const homeCategoryApiUrl = configData.HOME_CATEGORY_BASE_URL;
    const axiosConfig = {
        headers: {
            sessionToken: configData.SESSION_TOKEN,
        },
    };
    // Get post categories

    const adHeader = props.adHeader;

    const adsDataFooter = props.adFooter;
    const adImageFooter = adsDataFooter ? adsDataFooter.pAsset.AssetLiveUrl : '';
    const adFooterLink = adsDataFooter ? adsDataFooter.AdLink : '';

    const adsDataRight = props.adRight;
    const adImageRight = adsDataRight ? adsDataRight.pAsset.AssetLiveUrl : '';
    const adRightLink = adsDataRight ? adsDataRight.AdLink : '';

    const settingData = props.settingData;

    const [searchParams, setSearchParams] = useSearchParams();
    const searchText = searchParams.get("search");

    useEffect(() => {
        Axios.get(State_SLIDER_API_URL, axiosConfig).then((response) => {
            setStateSliderData(JSON.parse(response.data.payload));
        });

        Axios.get(State_RIGHT_SLIDER_API_URL, axiosConfig).then((response) => {
            setStateRightSliderData(JSON.parse(response.data.payload));
        });
        Axios.get(homeCategoryApiUrl, axiosConfig).then((response) => {
            setHomeCategory(JSON.parse(response.data.payload));
        });
    }, [])
    if (!homeCategory) return null;
    return (
        <div>
            {/* <!-- Top Header Area Start --> */}
            <TopHeader todayDate={props.todayDate} />
            {/* <!-- Top Header Area End --> */}

            {/* <!--Main-Menu Area Start--> */}
            <MainMenu menus={menus} />
            {/* <!--Main-Menu Area End--> */}

            {/* <!-- Mobile Menu Area Start --> */}
            <MobileMenu menus={menus} />
            {/* <!-- Mobile Menu Area End --> */}

            {/* <!-- Top Header Area Start --> */}
            <TopHeaderScroll marqueeNews={marqueeNews} site_lang={site_lang} />
            {/* <!-- Top Header Area End --> */}

            {/* <!-- Hero Area Start --> */}

            {/* state work area start ****************************************############################### */}


            {
                adHeader && adHeader.pAsset != '' ?
                    <AdArea adHeader={adHeader} />
                    :
                    <AdAreaSkeleton />
            }

            {(StateRightSliderData.length > 0 && StateSliderData.length > 0) ?
                <StateHeroArea stateslug={stateslug} rightSlider={StateRightSliderData} homeSlider={StateSliderData} />
                :
                <HeroAreaSkeleton />
            }

            {
                stateslug ?
                    <StatePlace1 stateslug={stateslug} homeCategory={homeCategory} />
                    :
                    <HomePlace1Skeleton />
            }


            <HomePlace2 homeCategory={homeCategory} />

            <HomePlace4 homeCategory={homeCategory} />
            {/* <!-- Aadhyatm Area Start --> */}
            <HomePlace5 homeCategory={homeCategory} adRight3={props.adRight3} />
            <HomePlace6 homeCategory={homeCategory} />


            <section className="home-front-area">
                <div className="container">
                    <div className="row">
                        <div className="col-lg-12">
                        <iframe className='youtubeVideo' frameBorder="0" scrolling="no" marginHeight="0" marginWidth="0"
                                type="text/html" src={settingData.YoutubeVideoURL}></iframe>
                        </div>
                    </div>
                </div>
            </section>
            <HomePlace7 homeCategory={homeCategory} />
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


            {/* state work area end ****************************************############################### */}

            {searchText &&
                <SearchList />
            }
            {/* <!-- Hero Area End --> */}

            {/* <!--  देश विदेश Area End --> */}

            {/* <!-- Footer Area Start --> */}
            <Footer footerMenu={props.footerMenu} settingData={settingData} />
            {/* <!-- Footer Area End --> */}

            {/* <!-- Back to Top Start --> */}
            <div className="bottomtotop">
                <i className="fas fa-chevron-right"></i>
            </div>
            {/* <!-- Back to Top End --> */}

        </div>
    )
}




