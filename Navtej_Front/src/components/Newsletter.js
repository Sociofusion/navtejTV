import React, { Fragment, props } from 'react'
import parse from "html-react-parser";
import configData from './Config';
import Axios from "axios";


export default function Newsletter(props) {
    const newsletterApi = configData.NEWSLETTER_API;

    const [success, setSuccess] = React.useState(false);
    const axiosConfig = {
        headers: {
            sessionToken: configData.SESSION_TOKEN,
        },
    };
    const adsDataRight3 = props.adRight3;
    const adImageRight3 = adsDataRight3 ? adsDataRight3.pAsset.AssetLiveUrl : "";
    const adRightLink3 = adsDataRight3 ? adsDataRight3.AdLink : "";

    function submitNewsletter() {
        const email = document.getElementById('newsletter_email').value;
        const postData = { 'PersonEmail': email }
        Axios.post(newsletterApi, postData, axiosConfig).then((response) => {
            response && response.data.success ? setSuccess(true) : setSuccess(false);
        })
    }
    return (
        <div className="main-content tab-view border-theme mt-15 subscribeSpace">
            <div className="row">
                <div className="col-lg-12 mycol">
                    <div className="header-area">
                        <h3 className="title">
                            Subscribe Newsletter
                        </h3>
                    </div>
                </div>
            </div>
            <div className="row">
                <div className="col-lg-12 mycol">

                    <div className="aside-newsletter-widget mt-3 subarea">
                        <h5 className="title">Subscribe Newsletter!</h5>
                        <p>
                            Lorem Ipsum is simply dummy text of the printing and typesetting industry.
                            Lorem Ipsum has been the industry's standard dummy text ever since the 1500s.
                        </p>

                        <div className="input-group mb-3">
                            <input type="text" className="form-control" id="newsletter_email" required="true" placeholder="Email" aria-label="Email" aria-describedby="basic-addon2" />
                            <div className="input-group-append" onClick={submitNewsletter}>
                                <span className="input-group-text" id="basic-addon2"><i className="fa fa-arrow-right"></i></span>
                            </div>
                        </div>
                        {success && <span className='text-success'>Newsletter Submitted Successfully!</span>}

                    </div>

                    <div className="row">
                        <div className="col-lg-12">
                            <div className="ad-area">
                                <a href={adRightLink3} target="_blank">
                                    <img src={adImageRight3} style={{ marginBottom: "10px" }} />
                                </a>
                            </div>
                        </div>
                    </div>


                </div>
            </div>
        </div>
    )

}