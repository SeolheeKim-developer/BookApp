import Reac, { Component } from 'react';
export class BooksIndex extends Component {
    constructor(props) {
        super(props);

        this.state = {
            books: [],
            loading: false
        };
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
}