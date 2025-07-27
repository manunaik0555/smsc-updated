export async function load({ cookies }) {

    var token = cookies.get('token');
    var userName = cookies.get('userName');


    return {
        token, userName
    };
}