<?php
header('Access-Control-Allow-Origin:*');
header('Content-Type: application/json');

include_once('../../Config/db.php');
include_once('../../Model/Account.php');
include_once('../../Config/Common.php');

$_POST = json_decode(file_get_contents('php://input'), true);

$db = new Database();
$account = new Account($db->connect());

$account->username = isset($_POST["username"]) && !empty($_POST["username"]) ? $_POST["username"] : "";
$account->password = isset($_POST["password"]) && !empty($_POST["password"]) ? $_POST["password"] : "";
$name = isset($_POST["name"]) && !empty($_POST["name"]) ? $_POST["name"] : "";

$status = $account->InsertUser($account->username, $account->password, $name);

$array = [];

if ($status == 1) {
    $array = array(
        "status" => $SUCCESS,
        "data" => array(
            "userID" => $account->userID,
            "username" => $account->username,
            "role" => $account->role
        ),
        "message" => ""
    );
} else if ($status == 2) {
    $array = array(
        "status" => $FAIL,
        "message" => "Username already exists"
    );
} else if ($status == 3) {
    $array = array(
        "status" => $FAIL,
        "message" => "Register fail"
    );
} else if ($status == 4) {
    $array = array(
        "status" => $FAIL,
        "message" => "Param is empty"
    );
}

echo json_encode($array);
