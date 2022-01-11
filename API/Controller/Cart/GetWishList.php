<?php
header('Access-Control-Allow-Origin:*');
header('Content-Type: application/json');

include_once('../../Config/db.php');
include_once('../../Model/Cart.php');
include_once('../../Config/Common.php');

$_POST = json_decode(file_get_contents('php://input'), true);

$db = new Database();
$cart = new Cart($db->connect());

$userID = isset($_POST["userID"]) && !empty($_POST["userID"]) ? $_POST["userID"] : "";

$arrayProductWishList = $cart->GetCart($WISHLIST, $userID);

$array = [];

$array = array(
    "status" => $SUCCESS,
    "data" => $arrayProductWishList,
    "message" => "Get cart infomation"
);

echo json_encode($array);
