<?php
header('Access-Control-Allow-Origin:*');
header('Content-Type: application/json');

include_once('../../Config/db.php');
include_once('../../Model/Order.php');
include_once('../../Config/Common.php');

$_POST = json_decode(file_get_contents('php://input'), true);

$db = new Database();
$order = new Order($db->connect());

$userID = isset($_POST["userID"]) && !empty($_POST["userID"]) ? $_POST["userID"] : "";
$name = isset($_POST["name"]) && !empty($_POST["name"]) ? $_POST["name"] : "";
$email = isset($_POST["email"]) && !empty($_POST["email"]) ? $_POST["email"] : "";
$phone = isset($_POST["phone"]) && !empty($_POST["phone"]) ? $_POST["phone"] : "";
$address = isset($_POST["address"]) && !empty($_POST["address"]) ? $_POST["address"] : "";
$note = isset($_POST["note"]) && !empty($_POST["note"]) ? $_POST["note"] : "";
$totalPrice = isset($_POST["totalPrice"]) && !empty($_POST["totalPrice"]) ? $_POST["totalPrice"] : "";
// $cartID = isset($_POST["cartID"]) && !empty($_POST["cartID"]) ? $_POST["cartID"] : "";

$status = $order->InsertOrder($userID, $name, $email, $phone, $address, $note, $totalPrice);

$array = [];

if ($status == 1) {
    $array = array(
        "status" => $SUCCESS,
        "message" => "Add new order successfully!"
    );
} else {
    $array = array(
        "status" => $FAIL,
        "message" => "Add new order fail!"
    );
}

echo json_encode($array);
