<?php
header('Access-Control-Allow-Origin:*');
header('Content-Type: application/json');

include_once('../../Config/db.php');
include_once('../../Model/User.php');
include_once('../Common.php');

$_POST = json_decode(file_get_contents('php://input'), true);

$db = new Database();
$user = new User($db->connect());

$user->userID = isset($_POST["userID"]) ? $_POST["userID"] : die();
$user->name = isset($_POST["name"]) ? $_POST["name"] : die();
$user->email = isset($_POST["email"]) ? $_POST["email"] : die();
$user->gender = isset($_POST["gender"]) ? $_POST["gender"] : die();
$user->phone = isset($_POST["phone"]) ? $_POST["phone"] : die();
$user->address = isset($_POST["address"]) ? $_POST["address"] : die();

$status = $user->updateUser($user->userID, $user->name, $user->email, $user->gender, $user->phone, $user->address);

$array = [];

if ($status == 1) {
    $status1 = $user->getUser($user->userID);

    if ($status1 == 1) {
        $array = array(
            "status" => $SUCCESS,
            "data" => array(
                "userID" => $user->userID,
                "username" => $user->username,
                "role" => $user->role,
                "name" => $user->name,
                "email" => $user->email,
                "gender" => $user->gender,
                "phone" => $user->phone,
                "address" => $user->address
            ),
            "message" => "Update user information successfully"
        );
    } else if ($status == 2) {
        $array = array(
            "status" => $FAIL,
            "message" => "Update user information fail"
        );
    }
} else {
    $array = array(
        "status" => $FAIL,
        "message" => "Update user information fail"
    );
}

echo json_encode($array);
