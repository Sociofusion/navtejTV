import React from "react";
import HeroAreaCategory from "./HeroAreaCategory";
import SearchList from "./SearchList";
import MainMenu from "./MainMenu";
import MobileMenu from "./MobileMenu";
import TopHeader from "./TopHeader";
import TopHeaderScroll from "./TopHeaderScroll";
import { useSearchParams } from "react-router-dom";
import Footer from "./Footer";

export default function CategoryPage(props) {
    const menus = props.menus;
    const site_lang = props.site_lang;
    const marqueeNews = props.marqueeNews;
    const [searchParams, setSearchParams] = useSearchParams();
    const searchText = searchParams.get("search");
    const settingData = props.settingData;
    
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
            {!searchText &&
                <HeroAreaCategory adRight3={props.adRight3}
                 
                />
            }
            {searchText &&
                <SearchList />
            }
            {/* <!-- Hero Area End --> */}


            {/* <!--  देश विदेश Area End --> */}

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