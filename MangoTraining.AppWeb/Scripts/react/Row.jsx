class TableRow extends React.Component {
    constructor(props) {
        super(props);
    }

    render() {
        return (
            <li className="ms-ListItem is-selectable" tabIndex="0">
                <span className="ms-ListItem-primaryText">{this.props.store.Description}</span>
                <span className="ms-ListItem-secondaryText">{this.props.store.Responsable}</span>
                <div className="ms-ListItem-selectionTarget" />
                <div className="ms-ListItem-actions">
                    <div className="ms-ListItem-action">
                        <i className="ms-Icon ms-Icon--Delete" />
                    </div>
                    <div className="ms-ListItem-action">
                        <i className="ms-Icon ms-Icon--BulletedList" />
                    </div>
                </div>
            </li>
        );
    }
}