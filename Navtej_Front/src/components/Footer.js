import React, { Fragment } from 'react'
import configData from './Config';
import Axios from "axios";
export default function Footer(props) {
    const footerMenu = props.footerMenu;
    const settingData = props.settingData;
    const adminURL = configData.baseAdminUrl;
    const newsletterApi = configData.NEWSLETTER_API;
    const [success, setSuccess] = React.useState(false);


    const axiosConfig = {
        headers: {
            sessionToken: configData.SESSION_TOKEN,
        },
    };


    function submitNewsletter() {
        const email = document.getElementById('newsletter_email').value;

        const postData = { 'PersonEmail': email }
        Axios.post(newsletterApi, postData, axiosConfig).then((response) => {
            response && response.data.success ? setSuccess(true) : setSuccess(false);
        })
    }


    return (
        settingData ?
            <Fragment>
                <footer className="footer" id="footer">
                    <div className="container">
                        <div className="row">

                        </div>
                        <div className="row">
                            <div className="col-md-3 col-lg-3">
                                <div className="footer-widget about-widget">
                                    <div className="footer-logo">
                                        <a href="/">
                                            <img src={settingData.FooterLogoLiveUrl} alt="" />
                                        </a>
                                    </div>

                                    <div className="fotter-social-links">
                                        <ul>
                                            <li>
                                                <a href={settingData.FacebookLink} target="_blank" className="facebook mb-2">
                                                    <img src="/assets/images/social/facebook.png" />
                                                </a>
                                            </li>
                                            <li>
                                                <a href={settingData.TwitterLink} target="_blank" className="twitter mb-2">
                                                    <img src="/assets/images/social/twitter.png" />
                                                </a>
                                            </li>
                                            <li>
                                                <a href={settingData.YoutubeLink} target="_blank" className="youtube mb-2">
                                                    <img src="/assets/images/social/youtube.png" />
                                                </a>
                                            </li>
                                            <li>
                                                <a href={settingData.InstagramLink} target="_blank" className="instagram mb-2">
                                                    <img src="/assets/images/social/instagram.png" />
                                                </a>
                                            </li>
                                        </ul>
                                    </div>

                                </div>
                            </div>

                            <div className="col-md-2 col-lg-2">
                                <div className="footer-widget info-link-widget ilw1">
                                    <h4 className="title">
                                        USEFUL LINKS
                                    </h4>
                                    <ul className="link-list">
                                        {footerMenu && footerMenu.map((menu) => {
                                            return (

                                                <li>
                                                    <a href={`/${menu.Slug}`}>
                                                        <span>
                                                            {menu.MenuTitle}
                                                        </span>
                                                    </a>
                                                </li>
                                            )
                                        })}
                                    </ul>
                                </div>
                            </div>
                            <div className="col-md-4 col-lg-4">
                                <div className="footer-widget blog-widget contact-footer">

                                    <h4 className="title">
                                        <a href="#">Contact Us</a>
                                    </h4>

                                    <div className="f-contact">
                                        <ul>
                                            <li>
                                                <i className="fas fa-map-marker-alt"></i>
                                                <span>{settingData.Address}
                                                </span>
                                            </li>
                                            <li className='mt-45'>
                                                <i className="fas fa-phone"></i>
                                                <a href={"tel:" + settingData.Mobile1}>{settingData.Mobile1}</a> | &nbsp;
                                                <a href={"tel:" + settingData.Mobile2}>{settingData.Mobile2}</a>
                                            </li>
                                            <li className='mt-45'>
                                                <i className="fas fa-envelope"></i>
                                                <a className='text-theme' href={"mailto:" + settingData.MailID}>{settingData.MailID}</a>
                                            </li>
                                        </ul>
                                    </div>

                                </div>
                            </div>
                            <div className="col-md-3 col-lg-3">
                                <div className="social-links">
                                    <h4 className="title">
                                        Newsletter
                                    </h4>
                                </div><div className="f-contact">
                                    <i className="fas fa-envelope"></i>
                                    &nbsp;Subscribe for our daily news

                                </div>


                                <div className="news_letter">
                                    <div className="input-group mb-3">
                                        <input id="newsletter_email" type="text" required="true" className="form-control" placeholder="Email" aria-label="Email" aria-describedby="basic-addon2" />
                                        <div className="input-group-append" onClick={submitNewsletter}>
                                            <span className="input-group-text" id="basic-addon2">SUBSCRIBE</span>
                                        </div>
                                    </div>
                                    {success && <span className='text-success'>Newsletter Submitted Successfully!</span>}
                                </div>

                                <ul className="link-list">

                                    <li>
                                        <a href={adminURL ? adminURL : ''} target="_blank">
                                            <span>
                                                Admin | Reporter Login
                                            </span>
                                        </a>
                                    </li>
                                </ul>

                            </div>
                        </div>
                        <div className="copy-bg">
                            <div className="container">
                                <div className="row">
                                    <div className="col-lg-6 offset-md-3">
                                        <div className="content">
                                            <div className="content copyright">
                                                <p>
                                                    <a href="/">{settingData.Copyright}</a>
                                                </p>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </footer>
            </Fragment> : ''
    )

}