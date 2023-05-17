import Reac, { Component } from 'react';
import axios from 'axios';
export class BooksIndex extends Component {
    constructor(props) {
        super(props);

        this.state = {
            books: [],
            loading: true
        };
    }

    //OnInitialized()
    componentDidMount() {
        //this.populateBooksData();
        this.populateBooksDataWithAxios();
    }

    //display book list
    static renderBooksTable(books) {
        return(
            <table className="table table-striped" aria-labelledby="tableLabel">
                <thead>
                    <tr>
                        <th>Id</th>
                        <th>Title</th>
                        <th>-</th>
                        <th>-</th>
                    </tr>
                </thead>
                <tbody>
                    {books.map(book =>
                        <tr key={book.id}>
                            <td>{book.id}</td>
                            <td>{book.title}</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                    )}
                </tbody>
            </table>
        
        );
    }

    render() {
        let contents = this.state.loading
            ? <p><em>Loading....</em></p>
            : BooksIndex.renderBooksTable(this.state.books);
        return (
            <div>
                <h1>My Books</h1>
                <h2>written by yongjun Park</h2>
                { contents }
            </div>
        );
    }
    async populateBooksData() {
        //const response = await fetch('/api/Books');
        //const data = await response.json();
        //this.setState({ books: data, loading: false });

        try {
            const response = await fetch('/api/Books');
            if (!response.ok) {
                throw new Error('Failed to fetch books data');
            }
            const contentType = response.headers.get('content-type');
            if (!contentType || !contentType.includes('application/json')) {
                throw new Error('Invalid response format - expected JSON');
            }
            const data = await response.json();
            this.setState({ books: data, loading: false });
        } catch (error) {
            console.error(error);
            // Handle error state appropriately (e.g., display an error message or retry the request)
        }
    }

    async populateBooksDataWithAxios() {
        axios.get("/api/Books").then(response => {
            const data = response.data;
            this.setState({ books: data, loading: false });
        });
    }
}