<?php
header('Access-Control-Allow-Origin:*');
header('Content-Type: application/json');

include_once('../../Config/db.php');
include_once('../../Model/User.php');
include_once('../../Config/Common.php');

$_POST = json_decode(file_get_contents('php://input'), true);

$db = new Database();
$user = new User($db->connect());

$user->userID = isset($_POST["userID"]) && !empty($_POST["userID"]) ? $_POST["userID"] : "";

$status = $user->getUserInfo($user->userID);

$array = [];

if ($status == 1) {
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
        "message" => "User Information"
    );
} else if ($status == 2) {
    $array = array(
        "status" => $FAIL,
        "message" => ""
    );
}

echo json_encode($array);
