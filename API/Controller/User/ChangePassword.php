<?php
header('Access-Control-Allow-Origin:*');
header('Content-Type: application/json');

include_once('../../Config/db.php');
include_once('../../Model/Account.php');
include_once('../../Config/Common.php');

$_POST = json_decode(file_get_contents('php://input'), true);

$db = new Database();
$account = new Account($db->connect());

$account->userID = isset($_POST["userID"]) && !empty($_POST["userID"]) ? $_POST["userID"] : "";
$account->password = isset($_POST["password"]) && !empty($_POST["password"]) ? $_POST["password"] : "";
$newPassword = isset($_POST["newPassword"]) && !empty($_POST["newPassword"]) ? $_POST["newPassword"] : "";

$status = $account->ChangePassword($account->userID, $account->password, $newPassword);

$array = [];

if ($status == 1) {
    $array = array(
        "status" => $SUCCESS,
        "message" => "Change password successfully"
    );
} else if ($status == 2) {
    $array = array(
        "status" => $FAIL,
        "message" => "Password update failed"
    );
} else if ($status == 3) {
    $array = array(
        "status" => $FAIL,
        "message" => "The old password you have entered is incorrect"
    );
}

echo json_encode($array);
