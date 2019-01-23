class Table extends React.Component {
    constructor(props) {
        super(props);
        this.state = { stores: [] }
    }
    componentWillMount() {
        fetch('/api/Stores')
            .then((response) => {
                return response.json();
            })
            .then((stores) => {
                this.setState({ stores: stores });
            })
    }
    render() {

        if (this.state.stores.length > 0) {

            this.stores = this.state.stores; //[{ key: 1, title: 'item1' }, { key: 2, title: 'item2' }];
            const rows = [];

            this.stores.forEach(store => {
                rows.push(<TableRow store={store} key={store.key} />);
            });

            return (
                <ul className="ms-List">
                    {rows}
                </ul>
            );
        } else {
            return <p className="text-center">Cargando empleados...</p>
        }

        
    }
}

ReactDOM.render(<Table />, document.getElementById('content'));