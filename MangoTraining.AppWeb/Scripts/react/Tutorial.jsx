class CommentBox extends React.Component {
    render() {
        return (
            <ul className="ms-List">
                <li className="ms-ListItem is-unread is-selectable" tabIndex="0">
                    <span className="ms-ListItem-primaryText">Alton Lafferty</span>
                    <span className="ms-ListItem-secondaryText">Meeting notes</span>
                    <span className="ms-ListItem-tertiaryText">Today we discussed the importance of a, b, and c in regards to d.</span>
                    <span className="ms-ListItem-metaText">2:42p</span>
                    <div className="ms-ListItem-selectionTarget"></div>
                    <div className="ms-ListItem-actions">
                        <div className="ms-ListItem-action">
                            <i className="ms-Icon ms-Icon--Mail"></i>
                        </div>
                        <div className="ms-ListItem-action">
                            <i className="ms-Icon ms-Icon--Delete"></i>
                        </div>
                        <div className="ms-ListItem-action">
                            <i className="ms-Icon ms-Icon--Flag"></i>
                        </div>
                        <div className="ms-ListItem-action">
                            <i className="ms-Icon ms-Icon--Pinned"></i>
                        </div>
                    </div>
                </li>
                <li className="ms-ListItem is-unread is-selectable" tabIndex="0">
                    <span className="ms-ListItem-primaryText">Alton Lafferty</span>
                    <span className="ms-ListItem-secondaryText">Meeting notes</span>
                    <span className="ms-ListItem-tertiaryText">Today we discussed the importance of a, b, and c in regards to d.</span>
                    <span className="ms-ListItem-metaText">2:42p</span>
                    <div className="ms-ListItem-selectionTarget"></div>
                    <div className="ms-ListItem-actions">
                        <div className="ms-ListItem-action">
                            <i className="ms-Icon ms-Icon--Mail"></i>
                        </div>
                        <div className="ms-ListItem-action">
                            <i className="ms-Icon ms-Icon--Delete"></i>
                        </div>
                        <div className="ms-ListItem-action">
                            <i className="ms-Icon ms-Icon--Flag"></i>
                        </div>
                        <div className="ms-ListItem-action">
                            <i className="ms-Icon ms-Icon--Pinned"></i>
                        </div>
                    </div>
                </li>
                <li className="ms-ListItem is-selectable" tabIndex="0">
                    <span className="ms-ListItem-primaryText">Alton Lafferty</span>
                    <span className="ms-ListItem-secondaryText">Meeting notes</span>
                    <span className="ms-ListItem-tertiaryText">Today we discussed the importance of a, b, and c in regards to d.</span>
                    <span className="ms-ListItem-metaText">2:42p</span>
                    <div className="ms-ListItem-selectionTarget"></div>
                    <div className="ms-ListItem-actions">
                        <div className="ms-ListItem-action">
                            <i className="ms-Icon ms-Icon--Mail"></i>
                        </div>
                        <div className="ms-ListItem-action">
                            <i className="ms-Icon ms-Icon--Delete"></i>
                        </div>
                        <div className="ms-ListItem-action">
                            <i className="ms-Icon ms-Icon--Flag"></i>
                        </div>
                        <div className="ms-ListItem-action">
                            <i className="ms-Icon ms-Icon--Pinned"></i>
                        </div>
                    </div>
                </li>
                <li className="ms-ListItem is-selectable" tabIndex="0">
                    <span className="ms-ListItem-primaryText">Alton Lafferty</span>
                    <span className="ms-ListItem-secondaryText">Meeting notes</span>
                    <span className="ms-ListItem-tertiaryText">Today we discussed the importance of a, b, and c in regards to d.</span>
                    <span className="ms-ListItem-metaText">2:42p</span>
                    <div className="ms-ListItem-selectionTarget"></div>
                    <div className="ms-ListItem-actions">
                        <div className="ms-ListItem-action">
                            <i className="ms-Icon ms-Icon--Mail"></i>
                        </div>
                        <div className="ms-ListItem-action">
                            <i className="ms-Icon ms-Icon--Delete"></i>
                        </div>
                        <div className="ms-ListItem-action">
                            <i className="ms-Icon ms-Icon--Flag"></i>
                        </div>
                        <div className="ms-ListItem-action">
                            <i className="ms-Icon ms-Icon--Pinned"></i>
                        </div>
                    </div>
                </li>
            </ul>
        );
    }
}

ReactDOM.render(<CommentBox />, document.getElementById('content'));