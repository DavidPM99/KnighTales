<?php

use App\Http\Controllers\ChuckCantBeControlled;
use Illuminate\Http\Request;
use Illuminate\Support\Facades\Route;

/*
|--------------------------------------------------------------------------
| API Routes
|--------------------------------------------------------------------------
|
| Here is where you can register API routes for your application. These
| routes are loaded by the RouteServiceProvider within a group which
| is assigned the "api" middleware group. Enjoy building your API!
|
*/

Route::get('frases', [ChuckCantBeControlled::class, 'index']);
Route::get('frases/rand', [ChuckCantBeControlled::class, 'randomChuck']);