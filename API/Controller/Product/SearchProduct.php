<?php
header('Access-Control-Allow-Origin:*');
header('Content-Type: application/json');

include_once('../../Config/db.php');
include_once('../../Model/Product.php');
include_once('../../Config/Common.php');

// $_POST = json_decode(file_get_contents('php://input'), true);

$db = new Database();
$product = new Product($db->connect());

$keyword = isset($_GET["keyword"]) && !empty($_GET["keyword"]) ? $_GET["keyword"] : "";

$arrayProduct = $product->searchProduct($keyword);

$array = [];

$array = array(
    "status" => $SUCCESS,
    "data" => $arrayProduct,
    "message" => "Search Product"
);

echo json_encode($array);
