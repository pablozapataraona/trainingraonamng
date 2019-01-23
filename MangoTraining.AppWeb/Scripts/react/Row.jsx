class TableRow extends React.Component {
    constructor(props) {
        super(props);
    }

    render() {
        return (
            <li className="ms-ListItem is-selectable" tabIndex="0">
                <span className="ms-ListItem-primaryText">{this.props.store.Description}</span>
                <span className="ms-ListItem-secondaryText">Test</span>
                <span className="ms-ListItem-tertiaryText">Other test</span>
                <span className="ms-ListItem-metaText">2:42p</span>
                <div className="ms-ListItem-selectionTarget" />
                <div className="ms-ListItem-actions">
                    <div className="ms-ListItem-action">
                        <i className="ms-Icon ms-Icon--Mail" />
                    </div>
                    <div className="ms-ListItem-action">
                        <i className="ms-Icon ms-Icon--Delete" />
                    </div>
                    <div className="ms-ListItem-action">
                        <i className="ms-Icon ms-Icon--Flag" />
                    </div>
                    <div className="ms-ListItem-action">
                        <i className="ms-Icon ms-Icon--Pinned" />
                    </div>
                </div>
            </li>
        );
    }
}