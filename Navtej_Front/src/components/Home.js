import React, { useEffect } from 'react'
import 'owl.carousel/dist/assets/owl.carousel.min.css'
import 'owl.carousel/dist/assets/owl.theme.default.min.css'
import TopHeader from './TopHeader'
import MainMenu from './MainMenu'
import MobileMenu from './MobileMenu'
import TopHeaderScroll from './TopHeaderScroll'
import AdArea from './AdArea'
import HeroArea from './HeroArea'
import Footer from './Footer'
import Axios from "axios"
import HomePlace1 from './HomePlace1'
import configData from "./Config";
import HomePlace2 from './HomePlace2.js'
import HomePlace3 from './HomePlace3'
import HomePlace4 from './HomePlace4'
import HomePlace5 from './HomePlace5'
import HomePlace6 from './HomePlace6'
import HomePlace7 from './HomePlace7'
import HeroAreaSkeleton from './HeroAreaSkeleton'
import AdAreaSkeleton from './AdAreaSkeleton'
import HomePlace1Skeleton from './HomePlace1Skeleton'


export default function Home(props) {
    const menus = props.menus;
    const marqueeNews = props.marqueeNews;
    const site_lang = props.site_lang;
    const homeCategoryApiUrl = configData.HOME_CATEGORY_BASE_URL;
    const homeSlider = props.homeSlider;
    const adHeader = props.adHeader;
    const adsDataFooter = props.adFooter;
    const adImageFooter = adsDataFooter ? adsDataFooter.pAsset.AssetLiveUrl : '';
    const adFooterLink = adsDataFooter ? adsDataFooter.AdLink : '';
    const adsDataRight = props.adRight;

    const adImageRight = adsDataRight ? adsDataRight.pAsset.AssetLiveUrl : '';
    const adRightLink = adsDataRight ? adsDataRight.AdLink : '';

    const settingData = props.settingData;
    const axiosConfig = {
        headers: {
            sessionToken: configData.SESSION_TOKEN,
        },
    };
    const [homeCategory, setHomeCategory] = React.useState([]);
    // Get post categories
    useEffect(() => {
        Axios.get(homeCategoryApiUrl, axiosConfig).then((response) => {
            setHomeCategory(JSON.parse(response.data.payload));
        });
    }, [])

    if (!homeCategory) return null;
    return (
        <div>
            <TopHeader todayDate={props.todayDate} />
            
            <MainMenu menus={menus} site_lang={site_lang} />
            <MobileMenu menus={menus} site_lang={site_lang} />
            <TopHeaderScroll marqueeNews={marqueeNews} site_lang={site_lang} />
            {
                adHeader && adHeader.pAsset != '' ?
                    <AdArea adHeader={adHeader} />
                    :
                    <AdAreaSkeleton />
            }
            {
                homeSlider.length > 0 ?
                    <HeroArea rightSlider={props.homeRightSlider} homeSlider={homeSlider} />
                    :
                    <HeroAreaSkeleton />
            }
            {
                homeCategory.length > 0 ?
                    <HomePlace1 homeCategory={homeCategory} />
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
            <Footer footerMenu={props.footerMenu} settingData={settingData} />
            <div className="bottomtotop">
                <i className="fas fa-chevron-right"></i>
            </div>
        </div>
    )
}
